using Eshop.Data;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Components
{
    public class Navbar:ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public Navbar(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            return View(_context.Categorys.ToList());
        }
    }
}
