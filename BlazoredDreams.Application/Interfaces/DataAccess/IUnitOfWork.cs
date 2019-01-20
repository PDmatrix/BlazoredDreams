using System;

namespace BlazoredDreams.Application.Interfaces.DataAccess
{
	public interface IUnitOfWork : IDisposable
	{
		ICommentRepository CommentRepository { get; }
		IDreamRepository DreamRepository { get; }
		IPostRepository PostRepository { get; }
		ITagRepository TagRepository { get; }
		IUserLikesRepository UserLikesRepository { get; }
		IUserRepository UserRepository { get; }

		void Commit();
	}
}