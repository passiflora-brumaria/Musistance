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

        /// <summary>
        /// Dependency Injection constructor.
        /// </summary>
        public AuthController (IItchIntegration itch, IAuthService auth)
        {
            _itch = itch;
            _auth = auth;
        }

        /// <summary>
        /// Creates a new account using itch OAuth.
        /// </summary>
        /// <returns>An auth session with a challenge URL for profile import.</returns>
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

        /// <summary>
        /// Performs a login operation. If the profile is not yet ready for use, returns back the login data for future retry.
        /// </summary>
        /// <param name="id">User ID, used as a username.</param>
        /// <param name="vCode">Validation code obtained at signup (or previous login), used as a password.</param>
        /// <returns>If the profile is ready, the auth session, which includes an access token and the next validation code; otherwise, the input data.</returns>
        [HttpPost("login/{id}")]
        [ProducesResponseType(200,Type = typeof(AuthSessionDto))]
        public async Task<IActionResult> LoginAsync (int id, [FromBody] string vCode)
        {
            return Ok(await _auth.UseValidationCodeAsync(id,vCode));
        }
    }
}
