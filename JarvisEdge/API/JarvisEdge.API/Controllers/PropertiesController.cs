using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using JarvisEdge.ServiceInterfaces;

namespace JarvisEdge.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Authorize]
    [Route("[controller]/[action]")]
    public class PropertiesController : Controller
    {
        private IPropertyService propertyService;

        public PropertiesController(IPropertyService propertyService)
        {
            this.propertyService = propertyService;
        }
    }
}