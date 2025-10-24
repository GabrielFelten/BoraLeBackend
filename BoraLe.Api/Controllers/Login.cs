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

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] Register register)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(errors);
            }

            try
            {
                string id = await _service.Register(register);
                return Ok(new { message = "Usuário registrado com sucesso!", id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }                        
        }
    }
}
