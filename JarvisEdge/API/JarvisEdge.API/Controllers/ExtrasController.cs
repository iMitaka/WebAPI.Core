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

namespace JarvisEdge.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Authorize]
    [Route("[controller]/[action]")]
    public class ExtrasController : Controller
    {
        private IExtraService extraService;

        public ExtrasController(IExtraService extraService)
        {
            this.extraService = extraService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAllExtras()
        {
            var result = extraService.GetAllExtras();

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult CreateExtra([FromBody]ExtraPostModel model)
        {
            var result = extraService.CreateExtra(model);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult DeleteExtra(int id)
        {
            var result = extraService.DeleteExtra(id);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}