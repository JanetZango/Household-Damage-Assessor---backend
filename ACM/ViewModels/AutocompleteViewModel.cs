namespace ACM.ViewModels
{
    public class AutocompleteViewModel
    {
        public string query { get; set; }
        public List<AutocompleteViewModelData> suggestions { get; set; }
    }

    public class AutocompleteViewModelData
    {
        public string value { get; set; }
        public string data { get; set; }
    }
}
