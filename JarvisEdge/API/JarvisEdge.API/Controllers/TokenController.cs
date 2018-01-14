namespace JarvisEdge.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using System.Security.Claims;
    using JarvisEdge.Helpers.Jwt;
    using JarvisEdge.DataTransferModels.Account;

    [Route("token")]
    [AllowAnonymous]
    public class TokenController : Controller
    {
        private const int tokenExpiryMinutes = 60;

        [HttpPost]
        public IActionResult CreateToken([FromBody]LoginPostModel model)
        {
            var usernameFromDb = "jarvis-test";
            var passwordFromDb = "test";

            var otherUsernameFromDb = "JARVIS-EDGE";
            var otherPasswordFromDb = "test2";


            if (model.Username.ToLower() == usernameFromDb.ToLower() && model.Password == passwordFromDb ||
                model.Username.ToLower() == otherUsernameFromDb.ToLower() && model.Password == otherPasswordFromDb)
            {
                var token = GenerateTokenForUser(usernameFromDb);

                return Ok(token);
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