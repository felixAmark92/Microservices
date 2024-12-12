using Dataccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dataccess.Repositories.Feedbacks
{
    public class FeedbackRepository : Repository<Feedback>
    {
        public FeedbackRepository(FeedbackDbContext context, DbSet<Feedback> dbSet) : base(context, dbSet)
        {
        }

        public async Task AddAsync(Feedback feedback)
        {
            throw new NotImplementedException();
        }

        public async Task<Feedback?> GetByIdAsync(int feedbackId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Feedback>> GetByProductIdAsync(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
