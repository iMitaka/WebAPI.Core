using JarvisEdge.Data.Repositories;
using JarvisEdge.DataTransferModels.Curency;
using JarvisEdge.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JarvisEdge.Services
{
    public class CurencyService : ICurencyService
    {
        private readonly IUowData data;

        public CurencyService(IUowData data)
        {
            this.data = data;
        }

        public IQueryable<CurencyGetModel> GetCurencies()
        {
            return data.Curencies.All().Where(x => !x.Deleted).Select(x => new CurencyGetModel() {
                Id = x.Id,
                Name = x.Name
            });
        }

        public bool CreateCurency(CurencyPostModel model)
        {
            if (model.Name != null && model.Name.Length >= 1)
            {
                data.Curencies.Add(new Models.Curency() { Name = model.Name });
                data.SaveChanges();
                return true;
            }

            return false;
        }

        public bool DeleteCurency(int id)
        {
            var curency = data.Curencies.All().FirstOrDefault(x => !x.Deleted && x.Id == id);

            if (curency != null)
            {
                curency.Deleted = true;
                data.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
