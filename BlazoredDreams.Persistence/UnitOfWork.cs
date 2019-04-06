using System;
using System.Data;
using BlazoredDreams.Application.Interfaces.DataAccess;
using BlazoredDreams.Persistence.Repositories;
using Npgsql;

namespace BlazoredDreams.Persistence
{
	public class UnitOfWork : IUnitOfWork
	{
        private IDbTransaction _transaction;
        
        private IDreamRepository _dreamRepository;
        private ITagRepository _tagRepository;
        private ICommentRepository _commentRepository;
        private IUserRepository _userRepository;
        private IPostRepository _postRepository;
        
        private bool _disposed;
        
        public UnitOfWork(string connectionString) 
            : this(new NpgsqlConnection(connectionString))
        {
        }

        public UnitOfWork(IDbConnection dbConnection)
        {
            Connection = dbConnection;
            Connection.Open();
            _transaction = Connection.BeginTransaction();
            // Correct mapping of entities with underscores, e.g created_at with CreatedAt
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public ICommentRepository CommentRepository => 
            _commentRepository ?? (_commentRepository = new CommentRepository(_transaction));

        public IDreamRepository DreamRepository =>
            _dreamRepository ?? (_dreamRepository = new DreamRepository(_transaction));

        public IPostRepository PostRepository =>
            _postRepository ?? (_postRepository = new PostRepository(_transaction));

        public ITagRepository TagRepository =>
            _tagRepository ?? (_tagRepository = new TagRepository(_transaction));

        public IUserRepository UserRepository =>
            _userRepository ?? (_userRepository = new UserRepository(_transaction));
        
        public IDbConnection Connection { get; private set; }
        
        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = Connection.BeginTransaction();
                ResetRepositories();
            }
        }

        private void ResetRepositories()
        {
            _dreamRepository = null;
            _tagRepository = null;
            _commentRepository = null;
            _userRepository = null;
            _postRepository = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) 
                return;
            
            if(disposing)
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }
                if(Connection != null)
                {
                    Connection.Dispose();
                    Connection = null;
                }
            }
            _disposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }	
	}
}