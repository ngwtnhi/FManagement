using Microsoft.AspNetCore.Mvc;

namespace FreightMana.Controllers
{
    public class ContactUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
