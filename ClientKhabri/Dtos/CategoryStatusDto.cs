namespace ClientKhabri.Dtos
{
    public class CategoryStatusDto
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public bool IsActive { get; set; }

    }
}
