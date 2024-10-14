using ACM.ViewModels.Services.SystemConfigServiceFactory;


namespace ACM.ViewModels
{
    public class PublicTermsAndConditionsViewModel
    {

        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;
        internal ClaimsPrincipal _user;
        internal ISystemConfigService _systemConfig;
        public string Content { get; set; }

        internal void PopulateModel()
        {
            Content = _systemConfig.GetSystemConfigValue<string>(PublicEnums.SystemConfigurationList.KEY_TERMS_CONDITIONS.ToString());
        }

    
    }
}
