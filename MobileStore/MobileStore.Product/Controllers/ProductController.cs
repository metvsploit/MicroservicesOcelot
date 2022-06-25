using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobileStore.Domain.Entities;
using MobileStore.Product.Domain.Service;
using System.Linq;
using System.Threading.Tasks;

namespace MobileStore.Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ISmartphoneService _service;

        public ProductController(ISmartphoneService service) => _service = service;

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create(Smartphone smartphone)
        {
            var userMail = User.Claims.SingleOrDefault();

            if (userMail == null)
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest();

            var response = await _service.CreateSmartphone(smartphone);

            if(response.Data == null)
                return BadRequest();

            return Ok(response.Descripton);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAllSmartphone();

            if (response.Data != null)
                return Ok(response.Data);
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _service.GetSmartphoneById(id);

            if(response.Data != null)
                return Ok(response.Data);

            return BadRequest();
        }
    }
}
