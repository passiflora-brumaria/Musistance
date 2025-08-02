using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Musistance.Controllers.Basis;
using Musistance.Dto.Profile;
using Musistance.Services.Interfaces;

namespace Musistance.Controllers
{
    [Route("api/profiles")]
    [ApiController]
    [Authorize]
    public class ProfileController: BaseAuthedController
    {
        private readonly IProfileService _prof;

        public ProfileController (IProfileService prof)
        {
            _prof = prof;
        }

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

        [HttpGet("{id}")]
        [ProducesResponseType(200,Type = typeof(ProfileDto))]
        public async Task<IActionResult> GetProfileAsync (int id)
        {
            return Ok(await _prof.GetProfileByIdAsync(id));
        }
    }
}
