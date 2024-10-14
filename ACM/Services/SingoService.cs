using Newtonsoft.Json;
using ACM.Models;
using ACM.ViewModels;
using System.Net.Http;
using System.Threading.Tasks;
namespace ACM.Services
{
    public class SingoService : ISingoService
    {
        private readonly HttpClient _apiClient;
        public string ApiBaseUrl { get; set; }
        public string AccessToken { get; set; }
        public ErrorResponse Error { get; set; }

        public SingoService(HttpClient httpClient)
        {
            _apiClient = httpClient;
        }


        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
        {
            AuthenticateResponse response = new AuthenticateResponse();

            _apiClient.DefaultRequestHeaders.Clear();
            _apiClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + AccessToken);

            var requestContent = new StringContent(JsonConvert.SerializeObject(request), System.Text.Encoding.UTF8, "application/json");
            var responseContent = await _apiClient.PostAsync($"{ApiBaseUrl}/authenticate", requestContent);

            var jsonResponse = await responseContent.Content.ReadAsStringAsync();

            if (responseContent.StatusCode == System.Net.HttpStatusCode.OK)
            {
                response = JsonConvert.DeserializeObject<AuthenticateResponse>(jsonResponse);

                if (response != null)
                {
                    AccessToken = response.access_token;
                }

                Error = new ErrorResponse()
                {
                    IsError = false,
                    ErrorMessage = ""
                };

                return response;
            }
            else
            {
                Error = new ErrorResponse()
                {
                    IsError = true,
                    ErrorMessage = jsonResponse
                };

                return null;
            }
        }

        public async Task<AuthenticateResponse> SingoAuthenticate(AuthenticateRequest request)
        {
            AuthenticateResponse response = new AuthenticateResponse();

            _apiClient.DefaultRequestHeaders.Clear();
            _apiClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + AccessToken);

            var requestContent = new StringContent(JsonConvert.SerializeObject(request), System.Text.Encoding.UTF8, "application/json");
            var responseContent = await _apiClient.PostAsync($"{ApiBaseUrl}/authenticate", requestContent);

            var jsonResponse = await responseContent.Content.ReadAsStringAsync();

            if (responseContent.StatusCode == System.Net.HttpStatusCode.OK)
            {
                response = JsonConvert.DeserializeObject<AuthenticateResponse>(jsonResponse);

                if (response != null)
                {
                    AccessToken = response.access_token;
                }

                Error = new ErrorResponse()
                {
                    IsError = false,
                    ErrorMessage = ""
                };

                return response;
            }
            else
            {
                Error = new ErrorResponse()
                {
                    IsError = true,
                    ErrorMessage = jsonResponse
                };

                return null;
            }
        }



    }
}
