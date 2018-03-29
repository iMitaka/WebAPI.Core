using JarvisEdge.Data.Repositories;
using JarvisEdge.DataTransferModels.PropertyStatus;
using JarvisEdge.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JarvisEdge.Services
{
    public class PropertyStatusService : IPropertyStatusService
    {
        private readonly IUowData data;

        public PropertyStatusService(IUowData data)
        {
            this.data = data;
        }

        public IQueryable<PropertyStatusGetModel> GetpropertyStatuses()
        {
            return data.PropertyStatuses.All().Where(x => !x.Deleted).Select(x => new PropertyStatusGetModel()
            {
                Name = x.Name,
                Id = x.Id
            });
        }

        public bool CreatePropertyStatus(PropertyStatusPostModel model)
        {
            if (model.Name != null && model.Name.Length >= 1)
            {
                data.PropertyStatuses.Add(new Models.PropertyStatus()
                {
                    Name = model.Name
                });

                data.SaveChanges();

                return true;
            }

            return false;
        }

        public bool DeletePropertyStatus(int id)
        {
            var propertyStatus = data.PropertyStatuses.All().FirstOrDefault(x => x.Id == id && !x.Deleted);

            if (propertyStatus != null)
            {
                propertyStatus.Deleted = true;
                data.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
