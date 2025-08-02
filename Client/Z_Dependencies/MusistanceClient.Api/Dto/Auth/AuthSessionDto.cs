namespace MusistanceClient.Api.Dto.Auth
{
    /// <summary>
    /// DTO to receive from the server on an active auth session.
    /// </summary>
    public class AuthSessionDto
    {
        /// <summary>
        /// ID of the profile.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Next validation code needed in order to log in.
        /// </summary>
        public string? NextValidationCode { get; set; }

        /// <summary>
        /// Current access token. If null, the session is not successful.
        /// </summary>
        public string? AccessToken { get; set; }

        /// <summary>
        /// URL to open to complete signup, if any is appropriate.
        /// </summary>
        public string? ChallengeUrl { get; set; }
    }
}
