using Eshop.Data;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Components
{
    public class Trend : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public Trend(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            return View(_context.Products.ToList());
        }
    }
}
