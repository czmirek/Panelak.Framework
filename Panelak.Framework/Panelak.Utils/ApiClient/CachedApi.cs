namespace Panelak.Utils
{
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    public abstract class CachedApi : BaseApi
    {
        private readonly Dictionary<string, object> cache = new Dictionary<string, object>();
        public CachedApi(ILogger<CachedApi> logger) : base(logger)
        { }

        protected async Task<T> GetCachedResponseAsync<T>(string path, Method method, Action<RestRequest> append = null, [CallerMemberName] string caller = "")
            where T : class
        {
            string cacheKey = $"BaseApi_Caller_{caller}";

            if (method == Method.GET && cache.ContainsKey(cacheKey))
                return cache[cacheKey] as T;

            IRestResponse response = await GetRestResponseAsync(path, method, append);

            T responseModel = JsonConvert.DeserializeObject<T>(response.Content);

            if (method == Method.GET)
                cache.Add(cacheKey, responseModel);

            return responseModel;
        }

        protected async Task<string> GetCachedRawResponseAsync(string path, Method method, Action<RestRequest> append = null, [CallerMemberName] string caller = "")
        {
            string cacheKey = $"BaseApi_Caller_{caller}";

            if (method == Method.GET && cache.ContainsKey(cacheKey))
                return cache[cacheKey] as string;

            IRestResponse response = await GetRestResponseAsync(path, method, append);

            if (method == Method.GET)
                cache.Add(cacheKey, response.Content);

            return response.Content;
        }
    }
}
