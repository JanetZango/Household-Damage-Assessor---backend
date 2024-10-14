namespace ACM.ViewModels
{
    public class PaginationViewModel
    {
        public int TotalRecords { get; set; }
        public int Skip { get; set; }
        public int Top { get; set; }
        public bool Descending { get; set; }
        public string SortBy { get; set; }
    }
}
