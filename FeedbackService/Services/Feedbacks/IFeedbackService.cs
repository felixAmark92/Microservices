using Dataccess.Entities;
using Dataccess.Repositories.Feedbacks;
using FeedbackService.DTOs;

namespace FeedbackService.Services.Feedbacks
{
    public interface IFeedbackService
    {
        Task<Feedback> SubmitFeedbackAsync(FeedbackDto feedbackDto);
        Task<Feedback?> GetFeedbackByIdAsync(int feedbackId);
        Task<IEnumerable<Feedback>> GetFeedbackByProductIdAsync(int productId);
        Task<double> GetAverageRatingAsync(int productId);
    }
}
