using Dataccess.Entities;
using Dataccess.Repositories.Feedbacks;
using FeedbackService.DTOs;

namespace FeedbackService.Services.Feedbacks
{
    public class FeedbackService : IFeedbackService
    {
        private readonly FeedbackRepository _feedbackRepository;

        public FeedbackService(FeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }
        public FeedbackService() { }
        public async Task<double> GetAverageRatingAsync(int productId)
        {
            var feedbacks = await _feedbackRepository.GetByProductIdAsync(productId);
            if (!feedbacks.Any())
                return 0;

            return feedbacks.Average(f => f.Rating);
        }

        public async Task<Feedback?> GetFeedbackByIdAsync(int feedbackId)
        {
            return await _feedbackRepository.GetByIdAsync(feedbackId);
        }

        public async Task<IEnumerable<Feedback>> GetFeedbackByProductIdAsync(int productId)
        {
            return await _feedbackRepository.GetByProductIdAsync(productId);
        }

        public async Task<Feedback> SubmitFeedbackAsync(FeedbackDto feedbackDto)
        {
            var feedback = new Feedback
            {
                ProductId = feedbackDto.ProductId,
                Comment = feedbackDto.Comment,
                Rating = feedbackDto.Rating
            };

            await _feedbackRepository.AddAsync(feedback);
            return feedback;
        }
    }
}
