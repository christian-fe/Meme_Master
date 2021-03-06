
using Memes.Models;//*
using Microsoft.EntityFrameworkCore;//*

namespace Memes.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Photo> Photo { get; set; }
        public DbSet<User> User { get; set; }
    }
}
