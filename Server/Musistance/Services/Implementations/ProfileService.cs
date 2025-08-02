using Microsoft.EntityFrameworkCore;
using Musistance.Data.Contexts;
using Musistance.Data.Models.Users;
using Musistance.Dto.Profile;
using Musistance.Services.Interfaces;

namespace Musistance.Services.Implementations
{
    internal class ProfileService: IProfileService
    {
        private readonly MusistanceDbContext _db;

        public ProfileService (MusistanceDbContext context)
        {
            _db = context;
        }

        public async Task<ProfileDto> GetProfileByIdAsync (int id)
        {
            UserProfile up = await _db.Profiles.Where(p => p.Id == id).SingleAsync();
            return new ProfileDto()
            {
                UserId = up.Id,
                Name = up.Name,
                ProfilePicture = up.ProfilePicture
            };
        }
    }
}
