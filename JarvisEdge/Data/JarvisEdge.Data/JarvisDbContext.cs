namespace JarvisEdge.Data
{
    using JarvisEdge.Models;
    using Microsoft.EntityFrameworkCore;

    public class JarvisDbContext : DbContext
    {
        public JarvisDbContext(DbContextOptions<JarvisDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

    }
}
