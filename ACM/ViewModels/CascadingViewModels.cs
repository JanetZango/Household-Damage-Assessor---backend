namespace ACM.ViewModels
{
    public class CascadingViewModel
    {
        public string value { get; set; }
        public string label { get; set; }
    }

    public class CascadingSelectListModel
    {
        public List<CascadingViewModel> OptionsList { get; set; }
    }
}
