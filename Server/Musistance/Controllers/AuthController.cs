using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Musistance.AuthCallbacks.Itch.Interfaces;
using Musistance.Dto.Auth;
using Musistance.Services.Interfaces;

namespace Musistance.Controllers
{
    /// <summary>
    /// Authorisation and authentication operations.
    /// </summary>
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IItchIntegration _itch;
        private IAuthService _auth;

        public AuthController (IItchIntegration itch, IAuthService auth)
        {
            _itch = itch;
            _auth = auth;
        }

        [HttpGet("signup/itch")]
        [ProducesResponseType(200,Type = typeof(AuthSessionDto))]
        public async Task<IActionResult> ItchSignupAsync ()
        {
            int userId = await _auth.CreateEmptyAccountAsync();
            return Ok(new AuthSessionDto()
            {
                UserId = userId,
                ChallengeUrl = await _itch.GetOAuthUrlAsync(userId),
                NextValidationCode = await _auth.GenerateValidationCodeAsync(userId)
            });
        }

        [HttpPost("login/{id}")]
        [ProducesResponseType(200,Type = typeof(AuthSessionDto))]
        public async Task<IActionResult> LoginAsync (int id, [FromBody] string vCode)
        {
            return Ok(await _auth.UseValidationCodeAsync(id,vCode));
        }
    }
}
