using Microsoft.AspNetCore.Mvc;
using MobileStore.Authentication.Domain.Service;
using MobileStore.Domain.Entities;
using System.Threading.Tasks;

namespace MobileStore.Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAccountService _service;
        private readonly ITokenBuilder _tokenBuilder;

        public AuthenticationController(IAccountService service, ITokenBuilder tokenBuilder)
        {
            _service = service;
            _tokenBuilder = tokenBuilder;
        }
            

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User userModel)
        {
            var user = await _service.GetUserByEmail(userModel.Email);

            if (user.Data == null)
                return NotFound();

            var isValid = user.Data.Password == userModel.Password;

            if (!isValid)
                return BadRequest();

            var token = _tokenBuilder.BuildToken(user.Data.Email);

            return Ok(token);
        }
    }
}
