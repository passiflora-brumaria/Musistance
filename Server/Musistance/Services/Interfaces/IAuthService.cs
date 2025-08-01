using Musistance.Dto.Auth;

namespace Musistance.Services.Interfaces
{
    /// <summary>
    /// Manages auth concerns.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Creates an empty user account.
        /// </summary>
        /// <returns>The ID of the newly created user account.</returns>
        public Task<int> CreateEmptyAccountAsync ();

        /// <summary>
        /// Generates a validation code for a user account.
        /// </summary>
        /// <param name="userId">ID of the user account.</param>
        /// <returns>The generated validation code.</returns>
        public Task<string> GenerateValidationCodeAsync (int userId);

        /// <summary>
        /// Uses a validation code.
        /// </summary>
        /// <param name="userId">ID of the user account.</param>
        /// <param name="validationCode">Validation code to use.</param>
        /// <returns>The resulting session, with a valid JWT.</returns>
        public Task<AuthSessionDto> UseValidationCodeAsync (int userId, string  validationCode);
    }
}
