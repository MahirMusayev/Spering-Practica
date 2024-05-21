using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SperingTask.Models;

namespace SperingTask.DAL
{
    public class SperingContext : IdentityDbContext
    {
        public SperingContext(DbContextOptions<SperingContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
