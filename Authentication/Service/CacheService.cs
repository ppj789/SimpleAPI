using Microsoft.Extensions.Caching.Memory;
using SimpleAPI.Services;
using System.Linq;

namespace SimpleAPI.Authentication.Service
{
    public class CacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly UserService _userService;

        public CacheService(IMemoryCache memoryCache, UserService userService)
        {
            _memoryCache = memoryCache;
            _userService = userService;
        }

        public async ValueTask<int> GetClientIdFromApiKey(string apiKey)
        {
            if (!_memoryCache.TryGetValue<Dictionary<string, int>>($"Authentication_ApiKeys", out var internalKeys))
            {
                var users = await _userService.GetUsersAsync();
                if(users == null)
                {
                    return -1;
                }

                internalKeys = users.ToDictionary(user => user.ApiKey, user => user.Id);

                _memoryCache.Set($"Authentication_ApiKeys", internalKeys);
            }

            if (!internalKeys.TryGetValue(apiKey, out var clientId))
            {
                return -1;
            }

            return clientId;
        }
    }
}
