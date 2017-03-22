using Microsoft.EntityFrameworkCore;

namespace TwitterApi.Models
{
    public class TwitterContext : DbContext
    {
        public TwitterContext(DbContextOptions<TwitterContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<Following> Followings { get; set; }
    }
}