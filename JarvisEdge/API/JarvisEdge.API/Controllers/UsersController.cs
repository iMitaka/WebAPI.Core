namespace JarvisEdge.API.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using JarvisEdge.DataTransferModels.User;
    using JarvisEdge.ServiceInterfaces;
    using Microsoft.AspNetCore.Cors;
    using System.Linq;

    [EnableCors("CorsPolicy")]
    [Authorize]
    [Route("[controller]/[action]")]
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(
            IUserService userService
           )
        {
            this.userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm]UserLoginPostModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.LoginUser(model);

                if (!result.Error)
                {
                    return Ok(result.DataResult);
                }

                return BadRequest(result.ErrorMessage);
            }

            return BadRequest();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm]UserRegisterPostModel model)
        {
            if (ModelState.IsValid)
            {
                var registerFile = Request.Form.Files.Count > 0 ? Request.Form.Files.First() : null;
                var result = await userService.RegisterUser(model, registerFile);

                if (!result.Error)
                {
                    return Ok(result);
                }

                return BadRequest(result.ErrorMessage);
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await userService.LogoutUser();
            return Ok();
        }

        [HttpGet]
        public IActionResult CheckIdentity()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok(new { result = true });
            }

            return Unauthorized();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await userService.DeleteUser(id);

            if (!result.Error)
            {
                return Ok(result.DataResult);
            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var result = await userService.GetUserDetails(id);

            if (!result.Error)
            {
                return Ok(result.DataResult);
            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Modify(string id, [FromForm]UserModifyPostModel model)
        {
            var file = Request.Form.Files.Count > 0 ? Request.Form.Files.First() : null;
            var result = await userService.ModifyUser(model, id, file);

            if (!result.Error)
            {
                return Ok(result.DataResult);
            }

            return BadRequest(result.ErrorMessage);
        }
    }
}