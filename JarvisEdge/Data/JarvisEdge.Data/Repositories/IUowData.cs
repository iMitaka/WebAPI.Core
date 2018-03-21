namespace JarvisEdge.Data.Repositories
{
    using JarvisEdge.Models;

    public interface IUowData
    {
        IRepository<ApplicationUser> ApplicationUsers { get; }
        IRepository<Country> Countries { get; }
        IRepository<Town> Towns { get; }
        IRepository<Neighborhood> Neighborhoods { get; }

        int SaveChanges();
    }
}
