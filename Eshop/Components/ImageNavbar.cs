using Eshop.Data;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Components
{
    public class ImageNavbar:ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public ImageNavbar(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            return View(_context.Categorys.ToList());
        }
    }
}
