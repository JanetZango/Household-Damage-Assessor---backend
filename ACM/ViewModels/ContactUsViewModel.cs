using ACM.ViewModels.Services.SystemConfigServiceFactory;

namespace ACM.ViewModels
{
    public class ContactUsViewModel
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

        internal async Task Save()
        {
            var contatctUs = _context.SystemConfiguration.First(x => x.EventCode == PublicEnums.SystemConfigurationList.KEY_CONTACT_US.ToString());

            contatctUs.ConfigValue = Content;

            _context.Update(contatctUs);

            await _context.SaveChangesAsync();
        }
    }
}
