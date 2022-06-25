using Microsoft.AspNetCore.Mvc;
using MobileStore.Domain.Entities;
using MobileStore.Product.Helpers;

namespace MobileStore.Product.Controllers
{
    [Route("api/product/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
       [HttpPost("{quantity}")]
       public ActionResult AddProduct(Smartphone smartphone, int quantity)
       {
            if (SessionHelper.GetObjectFromJson<Cart>(HttpContext.Session, "cart")==null)
            {
                Cart cart = new Cart();
                cart.AddItem(smartphone, quantity);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                Cart cart = SessionHelper.GetObjectFromJson<Cart>(HttpContext.Session, "cart");
                cart.AddItem(smartphone, quantity);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return Ok();
       }

        [HttpGet]
        public IActionResult GetCart()
        {
            Cart cart = SessionHelper.GetObjectFromJson<Cart>(HttpContext.Session, "cart");

            if(cart == null)
                return NoContent();

            return Ok(cart.Lines);
        }

        [HttpDelete]
        public IActionResult RemoveLine(Smartphone smartphone)
        {
            Cart cart = SessionHelper.GetObjectFromJson<Cart>(HttpContext.Session, "cart");

            if (cart == null)
                return BadRequest();

            cart.RemoveLine(smartphone);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            return Ok();
        }

        [HttpPost]
        public IActionResult RemoveItem(Smartphone smartphone)
        {
            Cart cart = SessionHelper.GetObjectFromJson<Cart>(HttpContext.Session, "cart");

            if (cart == null)
                return BadRequest();

            cart.RemoveItem(smartphone);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            return Ok();
        }
    }
}
