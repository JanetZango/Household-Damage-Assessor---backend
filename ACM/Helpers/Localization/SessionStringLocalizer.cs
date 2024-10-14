using ACM.Models.SystemModelFactory;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Text;

namespace ACM.Helpers.Localization
{
    public class SessionStringLocalizer : IStringLocalizer
    {
        private List<LocalizationValue> _stringData;
        private IHttpContextAccessor _httpContextAccesor;
        private IDistributedCache _cache;

        public SessionStringLocalizer(IHttpContextAccessor httpContextAccesor, IDistributedCache cache)
        {
            _httpContextAccesor = httpContextAccesor;
            _stringData = new List<LocalizationValue>();
            _cache = cache;

            InitializeLocalizedStrings();
        }

        private void InitializeLocalizedStrings()
        {
            _stringData.Clear();

            if (_httpContextAccesor.HttpContext != null)
            {
                try
                {
                    Guid userID = Guid.Empty;

                    if (_httpContextAccesor.HttpContext.User != null && _httpContextAccesor.HttpContext.User.Identity.IsAuthenticated && _httpContextAccesor.HttpContext.User.Claims != null)
                    {
                        userID = Guid.Parse(_httpContextAccesor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/UserID").Value);

                        if (_httpContextAccesor.HttpContext.User.Claims.Any(x => x.Type == "CultureNameCode"))
                        {
                            var cultureNameCode = _httpContextAccesor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "CultureNameCode").Value;
                            var jsonBytes = _cache.Get("Localization_" + cultureNameCode.ToLower());

                            string jsonString = Encoding.UTF8.GetString(jsonBytes);
                            _stringData = JsonConvert.DeserializeObject<List<LocalizationValue>>(jsonString);
                        }
                    }
                    else
                    {
                        _stringData = new List<LocalizationValue>();
                    }

                }
                catch (Exception ex)
                {

                }
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return _stringData.Select(x => new LocalizedString(x.KeyName, x.Value, true)).ToList();
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return new SessionStringLocalizer(_httpContextAccesor, _cache);
        }

        public LocalizedString this[string name]
        {
            get
            {
                var translation = _stringData.FirstOrDefault(x => x.KeyName == name)?.Value;

                return new LocalizedString(name, translation ?? name, translation != null);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var translation = _stringData.FirstOrDefault(x => x.KeyName == name)?.Value;

                if (translation != null)
                {
                    translation = string.Format(translation, arguments);
                }

                return new LocalizedString(name, translation ?? name, translation != null);
            }
        }
    }
}
