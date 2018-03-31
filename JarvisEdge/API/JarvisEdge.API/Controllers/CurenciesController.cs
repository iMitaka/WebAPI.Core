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
using JarvisEdge.DataTransferModels.Curency;

namespace JarvisEdge.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Authorize]
    [Route("[controller]/[action]")]
    public class CurenciesController : Controller
    {
        private ICurencyService curencyService;

        public CurenciesController(ICurencyService curencyService)
        {
            this.curencyService = curencyService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetCurencies()
        {
            var result = curencyService.GetCurencies();

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult CreateCurency([FromBody]CurencyPostModel model)
        {
            var result = curencyService.CreateCurency(model);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult DeleteCurency(int id)
        {
            var result = curencyService.DeleteCurency(id);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}