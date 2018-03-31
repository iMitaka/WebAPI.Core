using JarvisEdge.Data.Repositories;
using JarvisEdge.DataTransferModels.Extra;
using JarvisEdge.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JarvisEdge.Services
{
    public class ExtraService : IExtraService
    {
        private readonly IUowData data;

        public ExtraService(IUowData data)
        {
            this.data = data;
        }

        public IQueryable<ExtraGetModel> GetAllExtras()
        {
            return data.Extras.All().Where(x => !x.Deleted).Select(x => new ExtraGetModel()
            {
                Id = x.Id,
                Name = x.Name
            });
        }

        public bool CreateExtra(ExtraPostModel model)
        {
            if (model.Name != null && model.Name.Length >= 1)
            {
                data.Extras.Add(new Models.Extra() { Name = model.Name });
                data.SaveChanges();
                return true;
            }

            return false;
        }

        public bool DeleteExtra(int id)
        {
            var extra = data.Extras.All().FirstOrDefault(x => !x.Deleted && x.Id == id);

            if (extra != null)
            {
                extra.Deleted = true;
                data.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
