namespace Blog.API.Application.Interfaces
{
	public interface IUnitOfWorkFactory
	{
		IUnitOfWork Create();
	}
}