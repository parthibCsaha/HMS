using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HMS.API.Controllers
{
    
    public class AuthController
    {
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register()
        {

        }
    }
}
