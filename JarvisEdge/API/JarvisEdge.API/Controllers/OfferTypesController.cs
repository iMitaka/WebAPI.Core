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

namespace JarvisEdge.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Authorize]
    [Route("[controller]/[action]")]
    public class OfferTypesController : Controller
    {
        private IOfferTypeService offerTypeService;

        public OfferTypesController(IOfferTypeService offerTypeService)
        {
            this.offerTypeService = offerTypeService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetOfferTypes()
        {
            var result = offerTypeService.GetOfferTypes();

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult CreateOfferType([FromBody]OfferTypePostModel model)
        {
            var result = offerTypeService.CreateOfferType(model);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult DeleteOfferType(int id)
        {
            var result = offerTypeService.DeleteOfferType(id);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}