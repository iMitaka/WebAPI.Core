using JarvisEdge.DataTransferModels.PropertyStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JarvisEdge.ServiceInterfaces
{
    public interface IPropertyStatusService
    {
        IQueryable<PropertyStatusGetModel> GetPropertyStatuses();
        bool CreatePropertyStatus(PropertyStatusPostModel model);
        bool DeletePropertyStatus(int id);
    }
}
