using JarvisEdge.DataTransferModels.PropertyType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JarvisEdge.ServiceInterfaces
{
    public interface IPropertyTypeService
    {
        IQueryable<PropertyTypeGetModel> GetPropertyTypes();
        bool CreatePropertyType(PropertyTypePostModel model);
        bool DeletePropertyType(int id);
    }
}
