using System;
using System.Diagnostics;
using Project.Network.Interface;

namespace Project.Network.Impementation
{
    public class ApiClient:IApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiClient> _logger;
        public ApiClient(HttpClient httpClient, ILogger<ApiClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> JsonGetDataAsync(string endPoint)
        {
            var httpResponse = await _httpClient.GetAsync(endPoint);
            _logger.LogInformation("{Fetch operation performed at {DateTime}", DateTime.UtcNow);
            return await parseHttpResponse(httpResponse);
        }

        private async Task<string> parseHttpResponse(HttpResponseMessage httpResponse)
        {
            if (!httpResponse.IsSuccessStatusCode)
            {
                var errorContent = await httpResponse.Content.ReadAsStringAsync();
                var message = $"*[{(int)httpResponse.StatusCode}] error occured at external api: {errorContent}";
                _logger.Log(LogLevel.Information, $"Error message : {message}");
                return errorContent;
                // throw new Exception(message);
            }

            var jsonString = await httpResponse.Content.ReadAsStringAsync();
            _logger.Log(LogLevel.Information, $"Response body content : {jsonString}");
            return jsonString;
        }

        
    }
}

