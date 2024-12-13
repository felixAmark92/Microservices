namespace FeedbackService.DTOs
{
    public class FeedbackDto
    {
        public int ProductId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; } // Rating 0-5
    }
}
