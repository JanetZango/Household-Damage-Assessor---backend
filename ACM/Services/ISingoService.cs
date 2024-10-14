using ACM.Models;
using ACM.ViewModels;
using System.Threading.Tasks;
namespace ACM.Services
{
    public interface ISingoService
    {
        public string ApiBaseUrl { get; set; }
        public string AccessToken { get; set; }
        public ErrorResponse Error { get; set; }
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest request);

        Task<AuthenticateResponse> SingoAuthenticate(AuthenticateRequest request);
    }
}
