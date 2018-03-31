using JarvisEdge.DataTransferModels.Neighborhood;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JarvisEdge.ServiceInterfaces
{
    public interface INeighborhoodService
    {
        IQueryable<NeighborhoodGetModel> GetNeighborhoodsByTownId(int townId);
        bool CreateNeighborhood(NeighborhoodPostModel model);
        bool DeleteNeighborhood(int id);
    }
}
