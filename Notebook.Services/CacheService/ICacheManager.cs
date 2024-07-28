using StackExchange.Redis;

namespace Notebook.Services.CacheService;

public interface ICacheManager
{
    Task<bool> Add(string key, string value);
    Task<bool> Update(string key, string value);
    Task<bool> Delete(string key);
    Task<string?> Get(string key);
    Task<bool> IsExists(string key);
}