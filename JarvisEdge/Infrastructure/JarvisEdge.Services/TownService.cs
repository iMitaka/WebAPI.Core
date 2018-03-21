using JarvisEdge.Data.Repositories;
using JarvisEdge.DataTransferModels.Town;
using JarvisEdge.Models;
using JarvisEdge.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JarvisEdge.Services
{
    public class TownService : ITownService
    {
        private readonly IUowData data;

        public TownService(IUowData data)
        {
            this.data = data;
        }

        public IQueryable<TownGetModel> GetTownsByCountryId(int countryId)
        {
            return this.data.Towns.All().Where(x => !x.Deleted && x.CountryId == countryId).Select(x => new TownGetModel()
            {
                Name = x.Name,
                Id = x.Id
            });
        }

        public bool CreateTown(TownPostModel model)
        {
            if (model.Name != null && model.Name.Length >= 1)
            {
                this.data.Towns.Add(new Town()
                {
                    CountryId = model.CountryId,
                    Name = model.Name
                });

                return true;
            }

            return false;
        }
    }
}
