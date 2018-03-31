using JarvisEdge.DataTransferModels.BuildingType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JarvisEdge.ServiceInterfaces
{
    public interface IBuildingTypeService
    {
        IQueryable<BuildingTypeGetModel> GetBuildingTypes();
        bool CreateBuildingType(BuildingTypePostModel model);
        bool DeleteBuldingType(int id);
    }
}
