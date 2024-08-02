using Microsoft.AspNetCore.Http;
using Notebook.Services.CacheService;
using Notebook.Services.ResultService;
using System.Text;

namespace Notebook.Services.AuthenticationServices;

public class SessionManager : ISessionManager
{
    private const string SessionProcessingError = "Error while processing session.";
    private const string SessionNotFoundError = "Session not found.";
    private const string AuthorizedError = "Unauthorized.";

    private readonly HttpContext _httpContext;
    private readonly ICacheManager _cacheManager;

    public SessionManager(IHttpContextAccessor contextAccessor,
        ICacheManager cacheManager)
    {
        _httpContext = contextAccessor.HttpContext;
        _cacheManager = cacheManager;
    }

    public void Append(string key, string value) => _httpContext.Session.Set(key, Encoding.UTF8.GetBytes(value));

    public Result<string> Get(string key)
    {
        if (!_httpContext.Session.TryGetValue(key, out byte[] byteValue))
        {
            return Result<string>.Error(AuthorizedError);
        }
        return Result<string>.Success(Encoding.UTF8.GetString(byteValue));
    }

    public void Remove(string key) => _httpContext.Session.Remove(key);
}