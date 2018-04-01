using JarvisEdge.Data.Repositories;
using JarvisEdge.DataTransferModels.BuildingType;
using JarvisEdge.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JarvisEdge.Services
{
    public class BuildingTypeService : IBuildingTypeService
    {
        private readonly IUowData data;

        public BuildingTypeService(IUowData data)
        {
            this.data = data;
        }

        public IQueryable<BuildingTypeGetModel> GetBuildingTypes()
        {
            return data.BuildingTypes.All().Where(x => !x.Deleted).Select(x => new BuildingTypeGetModel()
            {
                Id = x.Id,
                Name = x.Name
            }).OrderBy(x => x.Name);
        }

        public bool CreateBuildingType(BuildingTypePostModel model)
        {
            if (model.Name != null && model.Name.Length >= 1)
            {
                data.BuildingTypes.Add(new Models.BuildingType() { Name = model.Name });
                data.SaveChanges();
                return true;
            }

            return false;
        }

        public bool DeleteBuldingType(int id)
        {
            var buildingType = data.BuildingTypes.All().FirstOrDefault(x => !x.Deleted && x.Id == id);

            if (buildingType != null)
            {
                buildingType.Deleted = true;
                data.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
