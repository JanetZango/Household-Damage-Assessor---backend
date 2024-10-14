using ACM.ViewModels.Services.SystemConfigServiceFactory;

namespace ACM.ViewModels
{
    public class PublicContactUsViewModel
    {

        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;
        internal ClaimsPrincipal _user;
        internal ISystemConfigService _systemConfig;

        public string Content { get; set; }

        internal async Task PopulateModel()
        {
            Content = _systemConfig.GetSystemConfigValue<string>(PublicEnums.SystemConfigurationList.KEY_CONTACT_US.ToString());
        }

       
    }
}
