namespace JarvisEdge.Data
{
    using JarvisEdge.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class JarvisDbContext : IdentityDbContext<ApplicationUser>
    {
        public JarvisDbContext(DbContextOptions<JarvisDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
