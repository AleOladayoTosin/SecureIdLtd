using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SecureId.Ecommerce.ShoppingCart.Application.DTOs;
using SecureId.Ecommerce.ShoppingCart.Application.Interfaces;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace SecureId.Ecommerce.ShoppingCart.Infrastructure.Services
{
    public class BaseService : IBaseService
    {
        public IHttpClientFactory httpClient { get; set; }
        private readonly ILogger<BaseService> _logger;
        public BaseService(IHttpClientFactory httpClient, ILogger<BaseService> _logger)
        {
            this.httpClient = httpClient;
            this._logger = _logger;
        }
        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = httpClient.CreateClient("NotificationEngine");
                HttpRequestMessage msg = new HttpRequestMessage() { Version = new Version(2, 0) };
                msg.Headers.Add("accept", "application/json");
                msg.RequestUri = new Uri(apiRequest.Url);
                client.DefaultRequestHeaders.Clear();

                _logger.LogInformation($"{"URL" + " | " + apiRequest.Url + " | "}{DateTime.Now}");
                _logger.LogInformation($"{"Request" + " | " + JsonConvert.SerializeObject(apiRequest.Data) + " | "}{DateTime.Now}");
                if (apiRequest.Data != null)
                {
                    msg.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }

                if (!string.IsNullOrEmpty(apiRequest.AccessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.AccessToken);
                }
                HttpResponseMessage apiResponse = null;
                switch (apiRequest.ApiType)
                {
                    case ApiType.GET:
                        msg.Method = HttpMethod.Get;
                        break;
                    case ApiType.POST:
                        msg.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        msg.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        msg.Method = HttpMethod.Delete;
                        break;
                    default:
                        msg.Method = HttpMethod.Get;
                        break;
                }
                var stopwatch = Stopwatch.StartNew();
                apiResponse = await client.SendAsync(msg);
                stopwatch.Stop();
                _logger.LogInformation($"{"Request Time" + " | " + stopwatch + " | "}{DateTime.Now}");
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                _logger.LogInformation($"{"Response" + " | " + JsonConvert.SerializeObject(apiContent) + " | "}{DateTime.Now}");
                var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);
                return apiResponseDto;
            }
            catch (Exception e)
            {
                _logger.LogError($"{"Error" + " | " + JsonConvert.SerializeObject(e) + " | "}{DateTime.Now}");
                var res = JsonConvert.SerializeObject(e.Message);
                var apiResponseDto = JsonConvert.DeserializeObject<T>(res);
                return apiResponseDto;
            }

        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
