using Microsoft.AspNetCore.Mvc;
using BoraLe.Application.Interfaces;
using BoraLe.Domain.Entities;

namespace BoraLe.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _service;
        public LoginController(IUserService service) => _service = service;

        [HttpGet("Login")]
        public async Task<IActionResult> Login([FromQuery] string email, [FromQuery] string pass)
        {
            try
            {
                string id = await _service.Login(email, pass);
                return Ok(new { message = "Login realizado com sucesso!", id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }            
        }

        [HttpPost("UpsertUser")]
        public async Task<IActionResult> UpsertUser([FromBody] UpsertUser user)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                var errorMessage = string.Join(" ", errors);

                return BadRequest(new { message = errorMessage });
            }

            try
            {
                string id = await _service.UpsertUser(user);
                return Ok(new { message = "Usuário registrado com sucesso!", id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }                        
        }

        [HttpGet("GetUser")]
        public async Task<UserProfile> GetUser([FromQuery] string userId)
        {
            return await _service.GetUser(userId);
        }
    }
}
