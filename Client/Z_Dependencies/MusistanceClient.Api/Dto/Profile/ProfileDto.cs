namespace MusistanceClient.Api.Dto.Profile
{
    /// <summary>
    /// DTO on a user's profile.
    /// </summary>
    public class ProfileDto
    {
        /// <summary>
        /// User ID.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Display name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Profile picture.
        /// </summary>
        public byte[] ProfilePicture { get; set; }
    }
}