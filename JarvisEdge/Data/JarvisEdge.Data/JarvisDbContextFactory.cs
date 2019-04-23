namespace JarvisEdge.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    public class JarvisDbContextFactory : IDesignTimeDbContextFactory<JarvisDbContext>
    {
        public JarvisDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<JarvisDbContext>();
            optionsBuilder.UseSqlite("Data Source=homes.db");

            return new JarvisDbContext(optionsBuilder.Options);
        }
    }
}
