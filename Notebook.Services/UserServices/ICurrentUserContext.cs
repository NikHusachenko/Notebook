namespace Notebook.Services.UserServices;

public interface ICurrentUserContext
{
    Guid Id { get; }
    string Login { get; }
    string Email { get; }
    string FirstName { get; }
    string LastName { get; }
}