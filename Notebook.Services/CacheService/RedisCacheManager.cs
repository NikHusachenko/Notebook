using StackExchange.Redis;

namespace Notebook.Services.CacheService;

public sealed class RedisCacheManager : ICacheManager
{
    private readonly IDatabaseAsync _database;

    public RedisCacheManager(IConnectionMultiplexer connection)
    {
        _database = connection.GetDatabase();
    }

    public Task<bool> Add(string key, string value) => _database.StringSetAsync(key, value);

    public Task<bool> Delete(string key) => _database.KeyDeleteAsync(key);

    public async Task<string?> Get(string key) => await _database.StringGetAsync(key);

    public Task<bool> IsExists(string key) => _database.KeyExistsAsync(key);

    public async Task<bool> Update(string key, string value)
    {
        bool isDeleted = await _database.KeyDeleteAsync(key);
        if (isDeleted)
        {
            return await _database.StringSetAsync(key, value);
        }
        return false;
    }
}