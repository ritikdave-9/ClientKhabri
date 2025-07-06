namespace ClientKhabri.Dtos
{
    public class NewsSearchResultDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public List<News> Items { get; set; }
    }
}
