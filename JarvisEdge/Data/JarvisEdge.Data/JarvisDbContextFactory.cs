namespace JarvisEdge.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    public class JarvisDbContextFactory : IDesignTimeDbContextFactory<JarvisDbContext>
    {
        public JarvisDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<JarvisDbContext>();
            optionsBuilder.UseSqlServer(JarvisDbConstants.GetConnectionString());

            return new JarvisDbContext(optionsBuilder.Options);
        }
    }
}
