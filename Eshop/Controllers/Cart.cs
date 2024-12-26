using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers
{
    public class Cart : Controller
    {
        public IActionResult AddtoCart(int productid)
        {
            return View("Cart");
        }
    }
}
