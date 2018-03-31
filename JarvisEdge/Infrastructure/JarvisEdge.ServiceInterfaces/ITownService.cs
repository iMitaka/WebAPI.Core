using JarvisEdge.DataTransferModels.Town;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JarvisEdge.ServiceInterfaces
{
    public interface ITownService
    {
        IQueryable<TownGetModel> GetTownsByCountryId(int countryId);
        bool CreateTown(TownPostModel model);
        bool DeleteTown(int id);
    }
}
