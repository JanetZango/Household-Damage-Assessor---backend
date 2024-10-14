using ACM.ViewModels.Services.SystemConfigServiceFactory;
namespace ACM.ViewModels
{
    public class TermsAndConditionsViewModel
    {
        internal AppDBContext _context;
        internal SecurityOptions _securityOptions;
        internal ClaimsPrincipal _user;
        internal ISystemConfigService _systemConfig;

        public string Content { get; set; }

        internal async Task PopulateModel()
        {
            Content = _systemConfig.GetSystemConfigValue<string>(PublicEnums.SystemConfigurationList.KEY_TERMS_CONDITIONS.ToString());
        }

        internal async Task Save()
        {
            var contatctUs = _context.SystemConfiguration.First(x => x.EventCode == PublicEnums.SystemConfigurationList.KEY_TERMS_CONDITIONS.ToString());

            contatctUs.ConfigValue = Content;

            _context.Update(contatctUs);

            await _context.SaveChangesAsync();
        }
    }
}
