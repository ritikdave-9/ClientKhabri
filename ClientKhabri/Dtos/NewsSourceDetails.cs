using ClientKhabri.Enums;

namespace ClientKhabri.Dtos
{
    public class NewsSourceDetails
    {
        public int NewsSourceID { get; set; }
        public string Name { get; set; }
        public string BaseURL { get; set; }
        public NewsSourceStatus Status { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
