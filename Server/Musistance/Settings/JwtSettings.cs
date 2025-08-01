namespace Musistance.Settings
{
    /// <summary>
    /// Internal auth settings.
    /// </summary>
    internal class JwtSettings
    {
        /// <summary>
        /// Claim "iss".
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Claim "aud".
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Lifetime of JWTs, in hours.
        /// </summary>
        public int? LifetimeInHours { get; set; }

        /// <summary>
        /// String used to sign tokens.
        /// </summary>
        public string Signature { get; set; }
    }
}
