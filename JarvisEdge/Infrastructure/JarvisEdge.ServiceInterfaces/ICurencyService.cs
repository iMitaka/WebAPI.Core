using JarvisEdge.DataTransferModels.Curency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JarvisEdge.ServiceInterfaces
{
    public interface ICurencyService
    {
        IQueryable<CurencyGetModel> GetCurencies();
        bool CreateCurency(CurencyPostModel model);
        bool DeleteCurency(int id);
    }
}
