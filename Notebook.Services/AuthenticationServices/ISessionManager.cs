using Notebook.Services.ResultService;

namespace Notebook.Services.AuthenticationServices;

public interface ISessionManager
{
    void Append(string key, string value);
    void Remove(string key);
    Result<string> Get(string key);
}