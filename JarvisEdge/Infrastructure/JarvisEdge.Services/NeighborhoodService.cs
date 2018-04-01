using JarvisEdge.Data.Repositories;
using JarvisEdge.DataTransferModels.Neighborhood;
using JarvisEdge.Models;
using JarvisEdge.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JarvisEdge.Services
{
    public class NeighborhoodService : INeighborhoodService
    {
        private readonly IUowData data;

        public NeighborhoodService(IUowData data)
        {
            this.data = data;
        }

        public IQueryable<NeighborhoodGetModel> GetNeighborhoodsByTownId(int townId)
        {
            return this.data.Neighborhoods.All().Where(x => !x.Deleted && x.TownId == townId).Select(x => new NeighborhoodGetModel()
            {
                Name = x.Name,
                Id = x.Id
            }).OrderBy(x => x.Name);
        }

        public bool CreateNeighborhood(NeighborhoodPostModel model)
        {
            if (model.Name != null && model.Name.Length >= 1)
            {
                data.Neighborhoods.Add(new Neighborhood()
                {
                    Name = model.Name,
                    TownId = model.TownId
                });

                data.SaveChanges();

                return true;
            }
            return false;
        }

        public bool DeleteNeighborhood(int id)
        {
            var data = this.data.Neighborhoods.All().FirstOrDefault(x => x.Id == id && !x.Deleted);

            if (data != null)
            {
                data.Deleted = true;
                this.data.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
