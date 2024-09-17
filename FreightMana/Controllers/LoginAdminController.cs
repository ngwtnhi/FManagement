using FreightMana.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
namespace FreightMana.Controllers
{
    public class LoginAdminController : Controller
    {
        ManaFreightmentContext db = new ManaFreightmentContext();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            var existingAdmin = db.WarehouseAccounts.FirstOrDefault(u => (u.Email == Email || u.Password == Password));

            if (existingAdmin == null)
            {
                ModelState.AddModelError("", "Email/số điện thoại hoặc mật khẩu không đúng.");
                return View();
            }

            HttpContext.Session.SetInt32("Admin", existingAdmin.Id);
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
