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
            optionsBuilder.UseSqlServer("Data Source =.\\SQLEXPRESS;Initial Catalog = HomesTest; Integrated Security = True; MultipleActiveResultSets=True");

            return new JarvisDbContext(optionsBuilder.Options);
        }
    }
}
