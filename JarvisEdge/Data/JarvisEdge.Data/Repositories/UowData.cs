namespace JarvisEdge.Data.Repositories
{
    using JarvisEdge.Models;
    using System;
    using System.Collections.Generic;

    public class UowData : IUowData
    {
        private readonly JarvisDbContext context;
        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public UowData(JarvisDbContext context)
        {
            this.context = context;
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);

                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        public IRepository<ApplicationUser> ApplicationUsers
        {
            get { return this.GetRepository<ApplicationUser>(); }
        }

        public IRepository<Country> Countries
        {
            get { return this.GetRepository<Country>(); }
        }

        public IRepository<Town> Towns
        {
            get { return this.GetRepository<Town>(); }
        }

        public IRepository<Neighborhood> Neighborhoods
        {
            get { return this.GetRepository<Neighborhood>(); }
        }
        public IRepository<OfferType> OfferTypes
        {
            get { return this.GetRepository<OfferType>(); }
        }
        public IRepository<Property> Properties
        {
            get { return this.GetRepository<Property>(); }
        }
        public IRepository<PropertyType> PropertyTypes
        {
            get { return this.GetRepository<PropertyType>(); }
        }
        public IRepository<PropertyStatus> PropertyStatuses
        {
            get { return this.GetRepository<PropertyStatus>(); }
        }
        public IRepository<Curency> Curencies
        {
            get { return this.GetRepository<Curency>(); }
        }
        public IRepository<BuildingType> BuildingTypes
        {
            get { return this.GetRepository<BuildingType>(); }
        }
        public IRepository<Extra> Extras
        {
            get { return this.GetRepository<Extra>(); }
        }
        public IRepository<Photo> Photos
        {
            get { return this.GetRepository<Photo>(); }
        }
    }
}
