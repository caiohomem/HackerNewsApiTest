using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace HackerNewsApiTest.Services
{
    public class HackerNewsApiService
    {
        public HttpClient _client { get; }
        private IMemoryCache _cache;

        public HackerNewsApiService(HttpClient client, IMemoryCache memoryCache)
        {
            _client = client;
            _cache = memoryCache;
        }

        public async Task<JArray> GetIdsBestStories()
        {
            return JArray.Parse(await _client.GetStringAsync("beststories.json"));
        }

        public async Task<JObject> GetStory(string id)
        {
            JObject cacheEntry;

            if (!_cache.TryGetValue(id, out cacheEntry))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(10));

                var story = JObject.Parse(await _client.GetStringAsync(string.Format("item/{0}.json", id)));
                _cache.Set(id, story, cacheEntryOptions);
            }

            return cacheEntry;
        }
    }
}
