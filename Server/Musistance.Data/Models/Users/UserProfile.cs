using Musistance.Data.Models.Basis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musistance.Data.Models.Users
{
    /// <summary>
    /// User profile in the game.
    /// </summary>
    public class UserProfile: BaseModel
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public UserProfile (): base() { }

        /// <summary>
        /// Source of this profile. 0 for itch.
        /// </summary>
        public int? Source { get; set; }

        /// <summary>
        /// ID of the profile in the source.
        /// </summary>
        public int? SourceId { get; set; }

        /// <summary>
        /// Hash of the latest validation code.
        /// </summary>
        public string LatestValidatonCode { get; set; }

        /// <summary>
        /// Hashing salt of the latest validation code.
        /// </summary>
        public string LatestValidationSalt { get; set; }

        /// <summary>
        /// Display name for the profile.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Profile picture.
        /// </summary>
        public byte[] ProfilePicture { get; set; }

        /// <summary>
        /// Previously existing account with the same source and source ID.
        /// </summary>
        /// <remarks>FOREIGN KEY.</remarks>
        [ForeignKey(nameof(Parent))]
        public int? ParentId { get; set; }

        /// <summary>
        /// Previously existing account with the same source and source ID.
        /// </summary>
        /// <remarks>NAVIGATION PROPERTY.</remarks>
        public UserProfile? Parent { get; set; }
    }
}
