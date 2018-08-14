namespace JarvisEdge.Data.Repositories
{
    using JarvisEdge.Models;

    public interface IUowData
    {
        IRepository<ApplicationUser> ApplicationUsers { get; }

        int SaveChanges();
    }
}
