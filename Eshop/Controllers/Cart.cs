using Eshop.Data;
using Eshop.Infastructure;
using Eshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Controllers
{
    public class CartController : Controller
    {
        public Cart? Cart { get; set; }
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View("Cart",HttpContext.Session.GetJson<Cart>("cart") );
                     
        }
        public IActionResult AddtoCart(int productid)
        {
            Product? product = _context.Products
     .FirstOrDefault(p => p.ProductId == productid);

            if (product != null)
            {
                 Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(product, 1);
                HttpContext.Session.SetJson("cart", Cart);
            }

            return View("Cart",Cart);
        }
        public IActionResult RemoveoneCart(int productid)
        {
            Product? product = _context.Products
     .FirstOrDefault(p => p.ProductId == productid);

            if (product != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(product, -1);
                HttpContext.Session.SetJson("cart", Cart);
            }

            return View("Cart", Cart);
        }
        public IActionResult RemoveCart(int productid)
        {
            Product? product = _context.Products
     .FirstOrDefault(p => p.ProductId == productid);

            if (product != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.RemoveLine(product);
                HttpContext.Session.SetJson("cart", Cart);
            }

            return View("Cart", Cart);
        }


    }
}
