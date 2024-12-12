using Dataccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dataccess.Repositories.Feedbacks
{
    public class FeedbackRepository : Repository<Feedback>
    {
        public FeedbackRepository(FeedbackDbContext context, DbSet<Feedback> dbSet) : base(context, dbSet)
        {
        }
    }
}
