namespace JarvisEdge.Data.Repositories
{
    using JarvisEdge.Models;

    public interface IUowData
    {
        IRepository<ApplicationUser> ApplicationUsers { get; }
        IRepository<Country> Countries { get; }
        IRepository<Town> Towns { get; }
        IRepository<Neighborhood> Neighborhoods { get; }
        IRepository<OfferType> OfferTypes { get; }
        IRepository<Property> Properties { get; }
        IRepository<PropertyType> PropertyTypes { get; }
        IRepository<PropertyStatus> PropertyStatuses { get; }

        int SaveChanges();
    }
}
