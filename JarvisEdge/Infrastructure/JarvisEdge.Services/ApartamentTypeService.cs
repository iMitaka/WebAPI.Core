using JarvisEdge.Data.Repositories;
using JarvisEdge.DataTransferModels.ApartamentType;
using JarvisEdge.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JarvisEdge.Services
{
    public class ApartamentTypeService : IApartamentTypeService
    {
        private readonly IUowData data;

        public ApartamentTypeService(IUowData data)
        {
            this.data = data;
        }

        public IQueryable<ApartamentTypeGetModel> GetApartamentTypes()
        {
            return data.ApartamentTypes.All().Where(x => !x.Deleted).Select(x => new ApartamentTypeGetModel()
            {
                Id = x.Id,
                Name = x.Name
            }).OrderBy(x => x.Name);
        }

        public bool CreateApartamentType(ApartamentTypePostModel model)
        {
            if (model.Name != null && model.Name.Length >= 1)
            {
                data.ApartamentTypes.Add(new Models.ApartamentType() { Name = model.Name });
                data.SaveChanges();
                return true;
            }

            return false;
        }

        public bool DeleteApartamentType(int id)
        {
            var apartamentType = data.ApartamentTypes.All().FirstOrDefault(x => x.Id == id && !x.Deleted);

            if (apartamentType != null)
            {
                apartamentType.Deleted = true;
                data.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
