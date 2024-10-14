using Microsoft.AspNetCore.SignalR;

namespace ACM.Helpers
{
    public class SignalRCustomUserIdProvider : IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/UserID")?.Value.ToLower();
        }
    }
}
