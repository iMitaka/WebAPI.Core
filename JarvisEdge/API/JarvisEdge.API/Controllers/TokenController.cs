namespace JarvisEdge.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using JarvisEdge.API.Helpers.JWT;
    using JarvisEdge.API.Models.Account;
    using System.Security.Claims;

    [Route("token")]
    [AllowAnonymous]
    public class TokenController : Controller
    {
        private const int tokenExpiryMinutes = 60;

        [HttpPost]
        public IActionResult CreateToken([FromBody]LoginModel model)
        {
            var usernameFromDb = "jarvis-edge";
            var passwordFromDb = "test";

            if (model.Username == usernameFromDb && model.Password == passwordFromDb)
            {
                var token = GenerateTokenForUser(usernameFromDb);

                return Ok(token.Value);
            }

            return Unauthorized();
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
    }
}