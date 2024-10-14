namespace ACM.ViewModels
{
    public class SearchParamatersViewModel
    {
        public List<SearchParamatersViewModelData> PageSearchData { get; set; }
    }

    public class SearchParamatersViewModelData
    {
        public string PageEventCode { get; set; }
        public object SearchParms { get; set; }
    }
}
