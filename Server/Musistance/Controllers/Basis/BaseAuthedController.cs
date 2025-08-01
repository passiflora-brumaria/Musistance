using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Musistance.Controllers.Basis
{
    public class BaseAuthedController: ControllerBase
    {
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
