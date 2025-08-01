using Microsoft.EntityFrameworkCore;
using Musistance.AuthCallbacks.Itch.Dto;
using Musistance.AuthCallbacks.Itch.Interfaces;
using Musistance.AuthCallbacks.Itch.Settings;
using Musistance.Data.Contexts;
using Musistance.Data.Models.Users;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace Musistance.AuthCallbacks.Itch.Implementations
{
    internal class ItchIntegration : IItchIntegration
    {
        private readonly ItchSettings _settings;
        private readonly MusistanceDbContext _db;

        public ItchIntegration (ItchSettings settings, MusistanceDbContext context)
        {
            _settings = settings;
            _db = context;
        }

        public async Task<string> GetOAuthUrlAsync(int userId)
        {
            return $"https://itch.io/user/oauth?client_id={HttpUtility.UrlEncode(_settings.ClientId)}&scope={HttpUtility.UrlEncode(_settings.Scope.Aggregate((a,b) => a + " " + b))}&response_type={HttpUtility.UrlEncode(_settings.ResponseType)}&redirect_uri={_settings.RedirectUri}&state={HttpUtility.UrlEncode(userId.ToString())}";
        }

        public async Task UpdateProfileFromItchAsync(int userId, string accessToken)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage resp = await client.GetAsync($"https://itch.io/api/1/{accessToken}/me");
            if (resp.IsSuccessStatusCode)
            {
                IDictionary<string,ItchIdentityDto> respData = JsonSerializer.Deserialize<IDictionary<string,ItchIdentityDto>>(await resp.Content.ReadAsStringAsync());
                if (respData.TryGetValue("user",out ItchIdentityDto itchId))
                {
                    UserProfile up = await _db.Profiles.Where(p => p.Id == userId).SingleOrDefaultAsync();
                    if (up != null)
                    {
                        byte[] pfp = await client.GetByteArrayAsync(itchId.PictureUrl);
                        up.Source = 0;
                        up.SourceId = itchId.Id;
                        up.Name = itchId.DisplayName;
                        up.ProfilePicture = pfp;
                        UserProfile parent = await _db.Profiles.Where(p => (p.Source == 0) && (p.SourceId == up.SourceId) && !p.ParentId.HasValue).FirstOrDefaultAsync();
                        if (parent != null)
                        {
                            up.ParentId = parent.Id;
                            parent.Name = itchId.DisplayName;
                            parent.ProfilePicture = pfp;
                            _db.Profiles.Update(parent);
                        }
                        _db.Profiles.Update(up);
                        await _db.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
