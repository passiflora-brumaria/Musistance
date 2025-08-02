using Musistance.Dto.Profile;

namespace Musistance.Services.Interfaces
{
    /// <summary>
    /// Manages user profiles.
    /// </summary>
    public interface IProfileService
    {
        /// <summary>
        /// Gets a profile given its user's ID.
        /// </summary>
        /// <param name="id">User ID.</param>
        /// <returns>Found profile.</returns>
        public Task<ProfileDto> GetProfileByIdAsync (int id);
    }
}
