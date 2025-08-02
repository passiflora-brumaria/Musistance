using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Musistance.Controllers.Basis
{
    /// <summary>
    /// Base class for API controllers which require auth.
    /// </summary>
    public class BaseAuthedController: ControllerBase
    {
        /// <summary>
        /// Gets the authed user's ID.
        /// </summary>
        /// <returns>The authed user's ID, if available.</returns>
        protected int? GetUserId ()
        {
            Claim? idClaim = User.Claims.Where(c => c.Type == "uid").FirstOrDefault();
            if ((idClaim != null) && !String.IsNullOrEmpty(idClaim.Value))
            {
                return int.Parse(idClaim.Value);
            }
            else
            {
                return null;
            }
        }
    }
}
