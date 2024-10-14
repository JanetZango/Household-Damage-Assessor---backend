using Hangfire.Dashboard;

namespace ACM.Helpers
{
    public class HangfireAuthorizeFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            return HtmlHelperExtensions.UserHasRole(PublicEnums.UserRoleList.ROLE_ADMINISTRATOR.ToString(), httpContext.User);
        }
    }
}
