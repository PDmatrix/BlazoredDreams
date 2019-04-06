using System;
using System.Data;

namespace BlazoredDreams.Application.Interfaces.DataAccess
{
	public interface IUnitOfWork : IDisposable
	{
		ICommentRepository CommentRepository { get; }
		IDreamRepository DreamRepository { get; }
		IPostRepository PostRepository { get; }
		ITagRepository TagRepository { get; }
		IUserRepository UserRepository { get; }
		IDbConnection Connection { get; }

		void Commit();
	}
}