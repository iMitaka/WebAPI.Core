using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using JarvisEdge.ServiceInterfaces;
using JarvisEdge.DataTransferModels.Property;

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

        [AllowAnonymous]
        [HttpPost]
        public IActionResult GetProperties([FromBody]PropertyFilter filter, int page, int totalCount, string type)
        {
            var result = propertyService.GetProperties(filter, page, totalCount);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        public IActionResult CreateProperty(string title)
        {
            var result = propertyService.CreateProperty(title);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        public IActionResult DeleteProperty(int id)
        {
            var result = propertyService.DeleteProperty(id);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        public IActionResult GetPropertyForEdit(int id)
        {
            var result = propertyService.GetPropertyForEdit(id);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult EditProperty([FromBody]PropertyPostModel model, int id)
        {
            var result = propertyService.EditProperty(model, id);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}