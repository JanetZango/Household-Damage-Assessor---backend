using ACM.ViewModels.Services;
using ACM.ViewModels.Services.SystemConfigServiceFactory;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace ACM.Services.ClickatellServiceFactory
{
    public interface IClickatellService
    {
        Task<ClickatellServiceResponse> SendMessage(ClickatellServiceRequest request);
    }

    public class ClickatellService : IClickatellService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly HttpClient _apiClient;
        private readonly ISystemConfigService _configService;

        public ClickatellService(IConfiguration configuration, HttpClient httpClient, IHttpContextAccessor httpContextAccesor, ISystemConfigService configService)
        {
            _configuration = configuration;
            _apiClient = httpClient;
            _httpContextAccesor = httpContextAccesor;
            _configService = configService;
        }

        public async Task<ClickatellServiceResponse> SendMessage(ClickatellServiceRequest request)
        {
            ClickatellServiceResponse response = new ClickatellServiceResponse();

            _apiClient.DefaultRequestHeaders.Clear();
            _apiClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", _configService.GetSystemConfigValue<string>(PublicEnums.SystemConfigurationList.KEY_CLICKATELLE_API_KEY.ToString()).Trim());

            var requestContent = new StringContent(JsonConvert.SerializeObject(request), System.Text.Encoding.UTF8, "application/json");

            string url = _configService.GetSystemConfigValue<string>(PublicEnums.SystemConfigurationList.KEY_CLICKATELLE_ENDPOINT.ToString());
            var responseContent = await _apiClient.PostAsync(url, requestContent);

            var jsonResponse = await responseContent.Content.ReadAsStringAsync();

            if (responseContent.IsSuccessStatusCode)
            {
                response = JsonConvert.DeserializeObject<ClickatellServiceResponse>(jsonResponse);
            }
            else
            {
                response = new ClickatellServiceResponse() { 
                    Error = new ClickatellServiceResponseMessageError() { 
                        Code = responseContent.StatusCode.ToString(),
                        Description = jsonResponse
                    },
                    Messages = new List<ClickatellServiceResponseMessage>()
                };
            }

            return response;
        }
    }
}
