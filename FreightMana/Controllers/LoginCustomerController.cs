using FreightMana.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
namespace FreightMana.Controllers
{
    public class LoginCustomerController : Controller
    {
        ManaFreightmentContext db = new ManaFreightmentContext();

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(CusAccount cusAccount, string Confirm_password)
        {
          
            if (cusAccount.Password != Confirm_password)
            {
                ModelState.AddModelError("Confirm_password", "Xác nhận mật khẩu không khớp.");
            }

            if (db.CusAccounts.Any(x => x.Email == cusAccount.Email))
            {
                ModelState.AddModelError("cusAccount.Email", "Email đã tồn tại.");
            }

            if (db.CusAccounts.Any(x => x.Username == cusAccount.Username))
            {
                ModelState.AddModelError("cusAccount.Username", "Tên đăng nhập đã tồn tại");
            }

            if (cusAccount.Email.Length < 5)
            {
                ModelState.AddModelError("cusAccount.Email", "Email quá ngắn.");
            }

            if (cusAccount.Username.Length < 1)
            {
                ModelState.AddModelError("cusAccount.Username", "Tên đăng nhập quá ngắn.");
            }

            if (cusAccount.PhoneNumber.Length != 10 || !cusAccount.PhoneNumber.StartsWith("0"))
            {
                ModelState.AddModelError("cusAccount.PhoneNumber", "Số điện thoại không hợp lệ.");
            }

            if (!ModelState.IsValid)
            {
                return View("Index", cusAccount);
            }

            try
            {
                db.CusAccounts.Add(cusAccount);
                db.SaveChanges();
                return RedirectToAction("Index", "Customer");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi lưu dữ liệu.");
                return View("Index", cusAccount);
            }
        }

        [HttpPost]
        public ActionResult Login(string loginIdentifier, string Password)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            var existingUser = db.CusAccounts.FirstOrDefault(u => (u.Email == loginIdentifier || u.PhoneNumber == loginIdentifier) && u.Password == Password);

            if (existingUser == null)
            {
                ModelState.AddModelError("", "Email/số điện thoại hoặc mật khẩu không đúng.");
                return View();
            }

            HttpContext.Session.SetInt32("UserId", existingUser.Id);
            return RedirectToAction("Index", "Customer");
        }

    }
}
