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
using JarvisEdge.DataTransferModels.ApartamentType;

namespace JarvisEdge.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Authorize]
    [Route("[controller]/[action]")]
    public class ApartamentTypesController : Controller
    {
        private IApartamentTypeService apartamentTypeService;

        public ApartamentTypesController(IApartamentTypeService apartamentTypeService)
        {
            this.apartamentTypeService = apartamentTypeService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetApartamentTypes()
        {
            var result = apartamentTypeService.GetApartamentTypes();

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult CreateApartamentType([FromBody]ApartamentTypePostModel model)
        {
            var result = apartamentTypeService.CreateApartamentType(model);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult DeleteApartamentType(int id)
        {
            var result = apartamentTypeService.DeleteApartamentType(id);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}