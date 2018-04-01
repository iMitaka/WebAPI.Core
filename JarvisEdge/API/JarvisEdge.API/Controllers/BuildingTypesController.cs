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

namespace JarvisEdge.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Authorize]
    [Route("[controller]/[action]")]
    public class BuildingTypesController : Controller
    {
        private IBuildingTypeService buildingTypeService;

        public BuildingTypesController(IBuildingTypeService buildingTypeService)
        {
            this.buildingTypeService = buildingTypeService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetBuildingTypes()
        {
            var result = buildingTypeService.GetBuildingTypes();

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult CreateBuildingType([FromBody]BuildingTypePostModel model)
        {
            var result = buildingTypeService.CreateBuildingType(model);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult DeleteBuldingType(int id)
        {
            var result = buildingTypeService.DeleteBuldingType(id);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}