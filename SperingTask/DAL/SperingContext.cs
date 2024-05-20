using Microsoft.EntityFrameworkCore;
using SperingTask.Models;

namespace SperingTask.DAL
{
    public class SperingContext : DbContext
    {
        public SperingContext(DbContextOptions<SperingContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
    }
}
