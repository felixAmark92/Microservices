using Dataccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dataccess
{
    public class FeedbackDbContext : DbContext
    {
        public FeedbackDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Feedback> Feedbacks { get; set; }
    }
}
