namespace Notebook.EntityFramework.Repositories;

public interface IRepositoryFactory
{
    CredentialsRepository NewCredentialsRepository();
    UserRepository NewUserRepository();
}