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

namespace JarvisEdge.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Authorize]
    [Route("[controller]/[action]")]
    public class CountriesController : Controller
    {
        private ICountryService countryService;

        public CountriesController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAllCountries()
        {
            var result = countryService.GetCountries();

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult CreateCountry([FromBody]CountryPostModel model)
        {
            var result = countryService.CreateCountry(model);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult DeleteCountry(int id)
        {
            var result = countryService.DeleteCountry(id);

            if (result)
            {
                return Ok(result);
            }

            return BadRequest();
        }
    }
}