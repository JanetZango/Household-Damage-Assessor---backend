using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;

namespace ACM.SignalRHubs
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme + "," + JwtBearerDefaults.AuthenticationScheme)]
    public class UIUpdateHub :  Hub
    {
        private IDistributedCache _cache;
        private readonly AppDBContext _context;

        public UIUpdateHub(IDistributedCache cache, AppDBContext context)
        {
            _cache = cache;
            _context = context;
        }
    }

    public class UIUpdateHubHelper
    {
        public IHubContext<UIUpdateHub> _uiUpdateHub { get; set; }
        public string EventCode { get; set; }
        public string Data { get; set; }

        public async Task SendUserUIUpdateNotification(Guid UserID)
        {
            await _uiUpdateHub.Clients.User(UserID.ToString()).SendAsync("UpdateUI", EventCode, Data);
        }

        public async Task SendBroadcastUIUpdateNotification()
        {
            await _uiUpdateHub.Clients.All.SendAsync("UpdateUI", EventCode, Data);
        }
    }
}
