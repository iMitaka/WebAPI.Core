using JarvisEdge.DataTransferModels.OfferType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JarvisEdge.ServiceInterfaces
{
    public interface IOfferTypeService
    {
        IQueryable<OfferTypeGetModel> GetOfferTypes();
        bool CreateOfferType(OfferTypePostModel model);
        bool DeleteOfferType(int id);
    }
}
