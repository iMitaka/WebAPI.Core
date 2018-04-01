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
using JarvisEdge.DataTransferModels.BuildingType;
using JarvisEdge.DataTransferModels.Extra;
using JarvisEdge.DataTransferModels.PropertyStatus;

namespace JarvisEdge.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Authorize]
    [Route("[controller]/[action]")]
    public class PropertyStatusesController : Controller
    {
        private IPropertyStatusService propertyStatusService;

        public PropertyStatusesController(IPropertyStatusService propertyStatusService)
        {
            this.propertyStatusService = propertyStatusService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetPropertyStatuses()
        {
            var result = propertyStatusService.GetPropertyStatuses();

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult CreatePropertyStatus([FromBody]PropertyStatusPostModel model)
        {
            var result = propertyStatusService.CreatePropertyStatus(model);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult DeletePropertyStatus(int id)
        {
            var result = propertyStatusService.DeletePropertyStatus(id);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}