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
using JarvisEdge.DataTransferModels.Town;

namespace JarvisEdge.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Authorize]
    [Route("[controller]/[action]")]
    public class TownsController : Controller
    {
        private ITownService townService;

        public TownsController(ITownService townService)
        {
            this.townService = townService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetTownsByCountryId(int countryId)
        {
            var result = townService.GetTownsByCountryId(countryId);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult CreateTown([FromBody]TownPostModel model)
        {
            var result = townService.CreateTown(model);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult DeleteTown(int id)
        {
            var result = townService.DeleteTown(id);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}