using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
