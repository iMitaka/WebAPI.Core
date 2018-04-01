using JarvisEdge.DataTransferModels.ApartamentType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JarvisEdge.ServiceInterfaces
{
    public interface IApartamentTypeService
    {
        IQueryable<ApartamentTypeGetModel> GetApartamentTypes();
        bool CreateApartamentType(ApartamentTypePostModel model);
        bool DeleteApartamentType(int id);
    }
}
