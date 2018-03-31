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
using JarvisEdge.DataTransferModels.Neighborhood;

namespace JarvisEdge.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Authorize]
    [Route("[controller]/[action]")]
    public class NeighborhoodsController : Controller
    {
        private INeighborhoodService neighborhoodService;

        public NeighborhoodsController(INeighborhoodService neighborhoodService)
        {
            this.neighborhoodService = neighborhoodService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetNeighborhoodsByTownId(int townId)
        {
            var result = neighborhoodService.GetNeighborhoodsByTownId(townId);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult CreateNeighborhood([FromBody]NeighborhoodPostModel model)
        {
            var result = neighborhoodService.CreateNeighborhood(model);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult DeleteNeighborhood(int id)
        {
            var result = neighborhoodService.DeleteNeighborhood(id);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}