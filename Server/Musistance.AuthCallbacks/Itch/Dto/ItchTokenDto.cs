namespace Musistance.AuthCallbacks.Itch.Dto
{
    /// <summary>
    /// Token to receive from itch.
    /// </summary>
    public class ItchTokenDto
    {
        /// <summary>
        /// ID of the user being authed on itch.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Itch access token.
        /// </summary>
        public string Token { get; set; }
    }
}
