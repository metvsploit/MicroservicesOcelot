using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobileStore.Customer.Domain.Service;
using MobileStore.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace MobileStore.Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service) => _service = service;

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> Create(Person person)
        {
            var userMail = User.Claims.SingleOrDefault();

            if (userMail == null)
                return Unauthorized();

            var response = await _service.CreateCustomer(person);

            return Ok(response.Description);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAllCustomers();

            if (response.Data != null)
                return Ok(response.Data);
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _service.GetCustomerById(id);

            if (response.Data != null)
                return Ok(response.Data);
            return BadRequest();
        }
    }
}
