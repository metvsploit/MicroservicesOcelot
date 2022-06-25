using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobileStore.Domain.Entities;
using MobileStore.Order.Domain.Service;
using System.Linq;
using System.Threading.Tasks;

namespace MobileStore.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;
      
        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Ordering order)
        {
            var userMail = User.Claims.SingleOrDefault();

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if (userMail == null)
                return Unauthorized();

            var response = await _service.CreateOrder(order);

            if(response.Data != null)
                return Ok(response.Description);

            return BadRequest(response.Description);
        }
    }
}
