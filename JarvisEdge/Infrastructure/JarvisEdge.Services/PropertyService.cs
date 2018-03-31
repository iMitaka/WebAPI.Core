using JarvisEdge.Data.Repositories;
using JarvisEdge.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JarvisEdge.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IUowData data;

        public PropertyService(IUowData data)
        {
            this.data = data;
        }
    }
}
