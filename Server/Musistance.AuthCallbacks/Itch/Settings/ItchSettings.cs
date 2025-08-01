namespace Musistance.AuthCallbacks.Itch.Settings
{
    /// <summary>
    /// Itch integration settings.
    /// </summary>
    internal class ItchSettings
    {
        /// <summary>
        /// Client ID.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Auth scopes/permissions.
        /// </summary>
        public string[] Scope { get; set; }

        /// <summary>
        /// Response type (auth mode).
        /// </summary>
        public string ResponseType { get; set; }

        /// <summary>
        /// OAUTH redirection URL.
        /// </summary>
        public string RedirectUri { get; set; }
    }
}
