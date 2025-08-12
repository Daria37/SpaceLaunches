// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text.Json;
// using System.Threading.Tasks;
// using StackExchange.Redis;

// namespace api.Service
// {
//     public class RedisCacheService
//     {
//         private readonly IDatabase _redisDb;

//     public RedisCacheService(IConnectionMultiplexer redis)
//     {
//         _redisDb = redis.GetDatabase();
//     }

//     public async Task<T?> GetFromCacheAsync<T>(string key)
//     {
//         var value = await _redisDb.StringGetAsync(key);
//         return value.HasValue ? JsonSerializer.Deserialize<T>(value!) : default;
//     }

//     public async Task SaveToCacheAsync(string key, object value, TimeSpan expiry)
//     {
//         var serializedValue = JsonSerializer.Serialize(value);
//         await _redisDb.StringSetAsync(key, serializedValue, expiry);
//     }

//     public async Task RemoveFromCacheAsync(string key)
//     {
//         await _redisDb.KeyDeleteAsync(key);
//     }
//     }
// }