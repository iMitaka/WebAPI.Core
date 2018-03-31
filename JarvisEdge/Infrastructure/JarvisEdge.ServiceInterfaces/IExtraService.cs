using JarvisEdge.DataTransferModels.Extra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JarvisEdge.ServiceInterfaces
{
    public interface IExtraService
    {
        IQueryable<ExtraGetModel> GetAllExtras();
        bool CreateExtra(ExtraPostModel model);
        bool DeleteExtra(int id);
    }
}
