using Microsoft.AspNetCore.Mvc;

namespace FreightMana.Controllers
{
    public class OurServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
