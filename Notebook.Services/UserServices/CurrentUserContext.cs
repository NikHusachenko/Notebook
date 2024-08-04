using Notebook.Services.AuthenticationServices;
using Notebook.Services.ResultService;

namespace Notebook.Services.UserServices;

public class CurrentUserContext(ISessionManager manager) : ICurrentUserContext
{
    private const string UnauthorizedError = "Unauthorized.";

    private const string SESSION_KEY_NAME = "Id";

    public Guid Id
    {
        get
        {
            Result<string> result = manager.Get(SESSION_KEY_NAME);
            if (result.ErrorMessages.Any() || !Guid.TryParse(result.Value, out Guid id))
            {
                throw new UnauthorizedAccessException(UnauthorizedError);
            }
            return id;
        }
    }
}