using JarvisEdge.DataTransferModels.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JarvisEdge.ServiceInterfaces
{
    public interface IPropertyService
    {
        int? CreateProperty(string name);
        IQueryable<PropertyGetModel> GetProperties(PropertyFilter filter, int page, int totalCount, string type);
        PropertyGetModel GetProperty(int id);
        PropertyPostModel GetPropertyForEdit(int id);
        bool EditProperty(PropertyPostModel model, int id);
        bool DeleteProperty(int id);
    }
}
