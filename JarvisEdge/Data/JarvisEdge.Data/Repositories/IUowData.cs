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
        IRepository<Curency> Curencies { get; }
        IRepository<BuildingType> BuildingTypes { get; }
        IRepository<Extra> Extras { get; }
        IRepository<Photo> Photos { get; }
        IRepository<ApartamentType> ApartamentTypes { get; }

        int SaveChanges();
    }
}
