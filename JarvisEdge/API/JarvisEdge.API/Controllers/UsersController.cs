namespace JarvisEdge.API.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(User.Identity.IsAuthenticated);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            
            return Ok(User.Identity.Name);
        }
    }
}
