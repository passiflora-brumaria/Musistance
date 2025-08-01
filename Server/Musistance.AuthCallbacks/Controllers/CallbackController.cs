using Microsoft.AspNetCore.Mvc;
using Musistance.AuthCallbacks.Itch.Dto;
using Musistance.AuthCallbacks.Itch.Interfaces;

namespace Musistance.AuthCallbacks.Controllers
{
    public class CallbackController : Controller
    {
        private IItchIntegration _itch;

        public CallbackController (IItchIntegration itch)
        {
            _itch = itch;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error ()
        {
            return View();
        }

        public IActionResult Itch ()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ItchToken ([FromBody] ItchTokenDto token)
        {
            await _itch.UpdateProfileFromItchAsync(int.Parse(token.UserId),token.Token);
            // TODO. Get and Store Profile.
            // https://itch.io/user/oauth?client_id=b4f990d4387814957ac4884b093d77e5&scope=profile%3Ame&response_type=token&redirect_uri=https%3A%2F%2F127.0.0.1%3A7109%2FCallback%2FItch
            return View();
        }
    }
}
