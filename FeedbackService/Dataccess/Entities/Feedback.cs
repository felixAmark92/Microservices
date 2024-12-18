namespace Dataccess.Entities
{
    public class Feedback
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? Comment { get; set; }
        public int Rating { get; set; } // 0-5 stars
    }
}
