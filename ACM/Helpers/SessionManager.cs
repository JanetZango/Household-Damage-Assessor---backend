using Microsoft.AspNetCore.Http;

namespace ACM.Helpers
{
    public class SessionManager
    {
        private const string _SearchParameters = "SearchParameters";
        internal ISession session;

        public SearchParamatersViewModel SearchParameters
        {
            get
            {
                if (session.Keys.Contains(_SearchParameters))
                {
                    return JsonConvert.DeserializeObject<SearchParamatersViewModel>(session.GetString(_SearchParameters));
                }
                else
                {
                    return new SearchParamatersViewModel() { PageSearchData = new List<SearchParamatersViewModelData>() };
                }
            }
            set
            {
                session.SetString(_SearchParameters, JsonConvert.SerializeObject(value));
            }
        }
    }
}
