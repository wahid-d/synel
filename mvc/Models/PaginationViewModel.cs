namespace mvc.Models
{
    public class PaginationViewModel
    {
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }

        public PaginationViewModel(int totalPages, int currentPage)
        {
            TotalPages = totalPages;
            CurrentPage = currentPage;
        }
    }
}