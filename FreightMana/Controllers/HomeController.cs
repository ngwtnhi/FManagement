using Microsoft.AspNetCore.Mvc;

namespace FreightMana.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
