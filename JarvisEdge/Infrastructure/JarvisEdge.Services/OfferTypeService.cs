using JarvisEdge.Data.Repositories;
using JarvisEdge.DataTransferModels.OfferType;
using JarvisEdge.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JarvisEdge.Services
{
    public class OfferTypeService : IOfferTypeService
    {
        private readonly IUowData data;

        public OfferTypeService(IUowData data)
        {
            this.data = data;
        }

        public IQueryable<OfferTypeGetModel> GetOfferTypes()
        {
            return this.data.OfferTypes.All().Where(x => !x.Deleted).Select(x => new OfferTypeGetModel()
            {
                Id = x.Id,
                Name = x.Name
            }).OrderBy(x => x.Name);
        }

        public bool CreateOfferType(OfferTypePostModel model)
        {
            if (model.Name != null && model.Name.Length >= 1)
            {
                this.data.OfferTypes.Add(new Models.OfferType()
                {
                    Name = model.Name
                });

                data.SaveChanges();

                return true;
            }

            return false;
        }

        public bool DeleteOfferType(int id)
        {
            var offerType = data.OfferTypes.All().FirstOrDefault(x => x.Id == id && !x.Deleted);

            if (offerType != null)
            {
                offerType.Deleted = true;
                data.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
