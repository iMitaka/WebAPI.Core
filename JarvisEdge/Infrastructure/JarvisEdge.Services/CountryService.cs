using JarvisEdge.Data.Repositories;
using JarvisEdge.DataTransferModels.Country;
using JarvisEdge.DataTransferModels.Town;
using JarvisEdge.Models;
using JarvisEdge.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JarvisEdge.Services
{
    public class CountryService : ICountryService
    {
        private readonly IUowData data;

        public CountryService(IUowData data)
        {
            this.data = data;
        }

        public bool CreateCountry(string name)
        {
            if (name != null && name.Length >= 1)
            {
                this.data.Countries.Add(new Country()
                {
                    Name = name
                });
            }
            return false;
        }

        public IQueryable<CountryGetModel> GetCountries()
        {
            return this.data.Countries.All().Where(x => !x.Deleted).Select(x => new CountryGetModel()
            {
                Name = x.Name,
                Id = x.Id
            });
        }
    }
}
