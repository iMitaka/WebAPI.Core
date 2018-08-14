namespace JarvisEdge.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    public class JarvisDbContextFactory : IDesignTimeDbContextFactory<JarvisDbContext>
    {
        public JarvisDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<JarvisDbContext>();
            optionsBuilder.UseSqlServer("Data Source =.\\SQLEXPRESS;Initial Catalog = JarvisDb; Integrated Security = True; MultipleActiveResultSets=True");

            return new JarvisDbContext(optionsBuilder.Options);
        }
    }
}
