using Notebook.Services.ResultService;

namespace Notebook.Services.AuthenticationServices;

public interface ISessionManager
{
    Task<Result> Append(string key, string value);
    Task<Result> Update(string key, string value);
    Task<Result> IsExists(string key);
    Task<Result> Remove(string key);
}