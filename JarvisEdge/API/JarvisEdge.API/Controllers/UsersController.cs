namespace JarvisEdge.API.Controllers
{
    using JarvisEdge.ServiceInterfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(userService.GetUserData());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            
            return Ok(User.Identity.Name);
        }
    }
}
