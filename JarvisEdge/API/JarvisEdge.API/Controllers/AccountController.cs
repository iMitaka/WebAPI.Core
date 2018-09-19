namespace JarvisEdge.API.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using JarvisEdge.Models;
    using JarvisEdge.DataTransferModels.Account;
    using JarvisEdge.Helpers.Jwt;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Cors;

    [EnableCors("CorsPolicy")]
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private const int tokenExpiryMinutes = 60;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _rolesManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> rolesManager
           )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _rolesManager = rolesManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginPostModel model)
        {
            string[] bannedUsers = { "test" };

            if (bannedUsers.Any(x => x.ToLower().Contains(model.Username.ToLower()))) return BadRequest();

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var token = GenerateTokenForUser(model.Username);
                    return Ok(token);
                }
                if (result.RequiresTwoFactor)
                {
                    return BadRequest("Requires Two Factor Authentication");
                }
                if (result.IsLockedOut)
                {
                    return BadRequest("Account Is Locked!");
                }
                else
                {
                    return Unauthorized();
                }
            }

            return BadRequest();
        }

        public IActionResult CheckIdentity()
        {
            if (User.Identity.IsAuthenticated)
            {
                return this.Ok(new { isLogged = true });
            }

            return this.BadRequest();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]RegisterPostModel model)
        {
            if (this.User.Identity.Name.ToLower() != "admin") return BadRequest();

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Username, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, MiddleName =  model.MiddleName, Phone = model.Phone };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return Ok("Account for " + model.Username + " successfuly created!");
                }
                AddErrors(result);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        private JwtToken GenerateTokenForUser(string username)
        {
            return new JwtTokenBuilder()
                                .AddSecurityKey(JwtSecurityKey.Create(JwtConstants.GetSigningKey()))
                                .AddSubject(username)
                                .AddIssuer(JwtConstants.GetIssuer())
                                .AddAudience(JwtConstants.GetAudience())
                                .AddClaim(ClaimTypes.Name, username)
                                .AddExpiry(tokenExpiryMinutes)
                                .Build();
        }

        private async void AddUserToRole(ApplicationUser user, string role)
        {
            var isRoleExist = await _rolesManager.RoleExistsAsync(role);
            if (!isRoleExist)
            {
                var roleResult = await _rolesManager.CreateAsync(new IdentityRole() { Name = role });
                if (roleResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
            }
            else
            {
                await _userManager.AddToRoleAsync(user, role);
            }

            var claims = User.Claims.Select(c => new
            {
                Type = c.Type,
                Value = c.Value
            });
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}