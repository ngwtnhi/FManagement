using Microsoft.AspNetCore.Mvc;

namespace FreightMana.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
