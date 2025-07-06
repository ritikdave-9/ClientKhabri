namespace ClientKhabri.Dtos
{
    public class NotificationDto
    {
        public int NotificationId { get; set; }
        public int NewsId { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime PublishedAt { get; set; }
    }
}
