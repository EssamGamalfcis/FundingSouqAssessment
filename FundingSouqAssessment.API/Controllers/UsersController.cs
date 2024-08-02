using FundingSouqAssessment.Models;
using FundingSouqAssessment.Application.Interfaces; 
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FundingSouqAssessment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _userService.RegisterAsync(model);
                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var token = await _userService.LoginAsync(model);

            if (token != null)
            {
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }
    }
}
