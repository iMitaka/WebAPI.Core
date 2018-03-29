using JarvisEdge.Data.Repositories;
using JarvisEdge.DataTransferModels.PropertyType;
using JarvisEdge.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JarvisEdge.Services
{
    public class PropertyTypeService : IPropertyTypeService
    {
        private readonly IUowData data;

        public PropertyTypeService(IUowData data)
        {
            this.data = data;
        }

        public IQueryable<PropertyTypeGetModel> GetPropertyTypes()
        {
            return data.PropertyTypes.All().Where(x => !x.Deleted).Select(x => new PropertyTypeGetModel()
            {
                Id = x.Id,
                Name = x.Name
            });
        }

        public bool CreatePropertyType(PropertyTypePostModel model)
        {
            if (model.Name != null && model.Name.Length >= 1)
            {
                data.PropertyTypes.Add(new Models.PropertyType()
                {
                    Name = model.Name
                });

                data.SaveChanges();
                return true;
            }

            return false;
        }

        public bool DeletePropertyType(int id)
        {
            var propertyType = data.PropertyTypes.All().FirstOrDefault(x => x.Id == id && !x.Deleted);

            if (propertyType != null)
            {
                propertyType.Deleted = true;
                data.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
