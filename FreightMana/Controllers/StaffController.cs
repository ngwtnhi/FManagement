using FreightMana.Models;
using Microsoft.AspNetCore.Mvc;

namespace FreightMana.Controllers
{
    public class StaffController : Controller
    {
        ManaFreightmentContext db = new ManaFreightmentContext();
        public IActionResult Index()
        {
            var listStaff = db.Staffs.ToList();
            ViewBag.staffList = listStaff;
            return View();
        }
        public ActionResult Confirm(Staff staff)
        {
            staff.WarehouseId = 1;
            db.Staffs.Add(staff);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Staff staff = db.Staffs.FirstOrDefault(s => s.Id == id);
            db.Staffs.Remove(staff);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult editStaff(int id)
        {
            var eStaff = db.Staffs.FirstOrDefault(s=>s.Id == id);
            return View(eStaff);
        }
        [HttpPost]

        public ActionResult ConfirmEdit(Staff staff)
        {   
            Staff sf = db.Staffs.FirstOrDefault(s => s.Id == staff.Id);
            sf.Id = staff.Id;
            sf.Position = staff.Position;
            sf.Name = staff.Name;
            sf.PhoneNumber = staff.PhoneNumber;
            sf.Email = staff.Email;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
