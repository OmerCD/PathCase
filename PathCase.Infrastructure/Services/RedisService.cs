using System;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace PathCase.Infrastructure.Services
{
    public class RedisService
    {
        private IDistributedCache _distributedCache;


        public RedisService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        private readonly DistributedCacheEntryOptions _options = new DistributedCacheEntryOptions()
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddDays(1),
        };
        public void SetAsJson<T>(string key, T val)
        {
            _distributedCache.SetString(key, JsonSerializer.Serialize(val), _options);
        }

        public T GetFromJson<T>(string key)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(_distributedCache.GetString(key));
            }
            catch
            {
                return default(T);
            }
        }
    }
}