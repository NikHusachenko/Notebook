using Notebook.Services.ResultService;

namespace Notebook.Services.AuthenticationServices;

public class SessionManager : ISessionManager
{
    public Task<Result> Append(string key, string value)
    {
        throw new NotImplementedException();
    }

    public Task<Result> IsExists(string key)
    {
        throw new NotImplementedException();
    }

    public Task<Result> Remove(string key)
    {
        throw new NotImplementedException();
    }

    public Task<Result> Update(string key, string value)
    {
        throw new NotImplementedException();
    }
}