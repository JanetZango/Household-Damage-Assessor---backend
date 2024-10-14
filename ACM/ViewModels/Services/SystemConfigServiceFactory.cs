namespace ACM.ViewModels.Services.SystemConfigServiceFactory
{
    public interface ISystemConfigService
    {
        T GetSystemConfigValue<T>(string eventCode);
    }

    public class SystemConfigService : ISystemConfigService
    {
        internal AppDBContext _context;

        public SystemConfigService(AppDBContext context)
        {
            _context = context;
        }

        public T GetSystemConfigValue<T>(string eventCode)
        {
            string configValue = "";

            configValue = _context.SystemConfiguration.FirstOrDefault(x => x.EventCode == eventCode)?.ConfigValue ?? "";

            if (!string.IsNullOrEmpty(configValue))
            {
                return (T)Convert.ChangeType(configValue, typeof(T));
            }
            else
            {
                return default(T);
            }
        }
    }
}
