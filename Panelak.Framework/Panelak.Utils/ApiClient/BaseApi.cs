namespace Panelak.Utils
{
    using Microsoft.Extensions.Logging;
    using System.Text.Json;
    using RestSharp;
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class BaseApi
    {
        private readonly ILogger logger;
        public BaseApi(ILogger logger)
            => this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

        protected async Task InvokeRequestAsync(string path, Method method, Action<RestRequest> append = null)
            => await GetRestResponseAsync(path, method, append);

        protected async Task<T> GetResponseAsync<T>(string path, Method method, Action<RestRequest> append = null)
            where T : class
        {
            IRestResponse response = await GetRestResponseAsync(path, method, append);
            T responseModel = JsonSerializer.Deserialize<T>(response.Content);
            return responseModel;
        }

        protected async Task<string> GetRawResponseAsync(string path, Method method, Action<RestRequest> append = null)
        {
            IRestResponse response = await GetRestResponseAsync(path, method, append);
            return response.Content;
        }

        protected async Task<IRestResponse> GetRestResponseAsync(string path, Method method, Action<RestRequest> append = null)
        {
            string url = GetBasePath() + "/" + path.TrimStart('/');
            logger.LogInformation($"API Request: {url}");

            var request = new RestRequest(url, method);

            string accessToken = GetAccessToken();
            if (accessToken != null)
                request.AddHeader("Authorization", "Bearer " + accessToken);

            append?.Invoke(request);

            var client = new RestClient();

            if(GetIgnoreSslErrors())
                client.RemoteCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            IRestResponse response = await client.ExecuteTaskAsync(request, new CancellationTokenSource().Token);

            logger.LogInformation($"API Response Status Code: {response.StatusCode}");

            if (response.ErrorException != null)
                logger.LogError(response.ErrorException, "API Response exception");

            if (!String.IsNullOrEmpty(response.ErrorMessage))
                logger.LogError("API ERROR MESSAGE:" + response.ErrorMessage);

            string logContent = response.Content;

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                logger.LogError($"API ERROR HTTP 500 ({url}) RESPONSE CONTENT: {response.Content}");
            }
            else
            {
                logger.LogInformation($"API Response content: {response.Content}");

                if (logContent.Length > 100)
                    logContent = logContent.Substring(0, 100) + " ... + " + (logContent.Length - 100).ToString() + " characters";

                logger.LogInformation($"API Response content: {logContent}");
            }

            if (accessToken != null)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                    throw new AccessTokenRejectedByApiException();
            }

            if ((int)response.StatusCode / 100 != 2)
                throw new InvalidApiStatusCodeException(response.StatusCode);

            return response;
        }

        protected abstract string GetBasePath();
        protected virtual string GetAccessToken() => null;

        protected virtual bool GetIgnoreSslErrors() => false;
    }
}
