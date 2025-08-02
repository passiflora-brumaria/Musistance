using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Musistance.Controllers.Basis;
using Musistance.Dto.Profile;
using Musistance.Services.Interfaces;

namespace Musistance.Controllers
{
    /// <summary>
    /// Operations for profile reading.
    /// </summary>
    [Route("api/profiles")]
    [ApiController]
    [Authorize]
    public class ProfileController: BaseAuthedController
    {
        private readonly IProfileService _prof;

        /// <summary>
        /// Dependency Injection constructor.
        /// </summary>
        public ProfileController (IProfileService prof)
        {
            _prof = prof;
        }

        /// <summary>
        /// Gets the authed user's profile.
        /// </summary>
        /// <returns>The authed user's profile.</returns>
        [HttpGet("self")]
        [ProducesResponseType(200,Type = typeof(ProfileDto))]
        public async Task<IActionResult> GetOwnProfileAsync ()
        {
            int? userId = GetUserId();
            if (userId.HasValue)
            {
                return Ok(await _prof.GetProfileByIdAsync(userId.Value));
            }
            else
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Gets a user's profile given their ID.
        /// </summary>
        /// <param name="id">ID of the user.</param>
        /// <returns>The profile, if found.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200,Type = typeof(ProfileDto))]
        public async Task<IActionResult> GetProfileAsync (int id)
        {
            return Ok(await _prof.GetProfileByIdAsync(id));
        }
    }
}
