using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using JarvisEdge.ServiceInterfaces;
using JarvisEdge.DataTransferModels.Country;
using JarvisEdge.DataTransferModels.OfferType;
using JarvisEdge.DataTransferModels.PropertyType;

namespace JarvisEdge.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Authorize]
    [Route("[controller]/[action]")]
    public class PropertyTypesController : Controller
    {
        private IPropertyTypeService propertyTypeService;

        public PropertyTypesController(IPropertyTypeService propertyTypeService)
        {
            this.propertyTypeService = propertyTypeService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetPropertyTypes()
        {
            var result = propertyTypeService.GetPropertyTypes();

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult CreatePropertyType([FromBody]PropertyTypePostModel model)
        {
            var result = propertyTypeService.CreatePropertyType(model);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult DeletePropertyType(int id)
        {
            var result = propertyTypeService.DeletePropertyType(id);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}