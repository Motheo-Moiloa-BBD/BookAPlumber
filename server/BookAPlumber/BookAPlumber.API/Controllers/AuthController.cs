using BookAPlumber.Core.Models.DTO;
using BookAPlumber.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookAPlumber.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            var result = await authService.RegisterUser(registerDTO);

            return Ok("User was registered successfully.");
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var result = await authService.LoginUser(loginDTO);

            return Ok(result);
        }
    }
}
