namespace Musistance.AuthCallbacks.Itch.Interfaces
{
    /// <summary>
    /// Manages Itch OAuth.
    /// </summary>
    public interface IItchIntegration
    {
        /// <summary>
        /// Gets the URL to begin the OAuth cycle.
        /// </summary>
        /// <param name="userId">ID of the user to authenticate.</param>
        public Task<string> GetOAuthUrlAsync (int userId);

        /// <summary>
        /// Updates a user profile from Itch.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <param name="accessToken">Itch access token to obtain profile data.</param>
        public Task UpdateProfileFromItchAsync (int userId, string accessToken);
    }
}
