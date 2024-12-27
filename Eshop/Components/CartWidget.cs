using Eshop.Data;
using Eshop.Infastructure;
using Eshop.Models;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Components
{
    public class CartWidget : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public CartWidget(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            return View(HttpContext.Session.GetJson<Cart>("cart") );
        }
    }
}
