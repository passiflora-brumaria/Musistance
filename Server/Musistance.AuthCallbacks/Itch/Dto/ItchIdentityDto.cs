using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Musistance.AuthCallbacks.Itch.Dto
{
    /// <summary>
    /// DTO to obtain from itch.io describing a user profile.
    /// </summary>
    public class ItchIdentityDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }
        [JsonPropertyName("cover_url")]
        public string PictureUrl { get; set; }
    }
}
