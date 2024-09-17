using FreightMana.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreightMana.Controllers
{
    public class TimeWorkController : Controller
    {
        ManaFreightmentContext db = new ManaFreightmentContext();
        public IActionResult Index()
        {
            var staffs = db.Staffs.ToList();
            ViewBag.Staffs = staffs;
            var shifts = db.Shifts.Include(e => e.Employee).OrderByDescending(e => e.Day).ToList();
            return View(shifts);
        }
        public IActionResult CreateShift(int staffId, String timeRange)
        {
            TimeOnly timeStart, timeEnd;
            switch (timeRange)
            {
                case "morning":
                    timeStart = new TimeOnly(7, 0);
                    timeEnd = new TimeOnly(11, 0);
                    break;
                case "afternoon":
                    timeStart = new TimeOnly(12, 0);
                    timeEnd = new TimeOnly(17, 0);
                    break;
                case "evening":
                    timeStart = new TimeOnly(18, 0);
                    timeEnd = new TimeOnly(22, 0);
                    break;
                default:
                    timeStart = new TimeOnly(7, 0);
                    timeEnd = new TimeOnly(11, 0);
                    break;
            }
            var shiftsOfDay = db.Shifts.Where(s => s.EmployeeId == staffId && s.Day == DateOnly.FromDateTime(DateTime.Now)).ToList();
            bool shiftConflict = shiftsOfDay.Any(s => (s.TimeStart < timeEnd && s.TimeEnd > timeStart));

            if (shiftConflict)
            {
                TempData["ErrorMessage"] = "Trùng ca làm trong ngày!";
                return RedirectToAction("Index");
            }

            db.Shifts.Add(new Shift()
            {
                Day = DateOnly.FromDateTime(DateTime.Now),
                EmployeeId = staffId,
                TimeStart = timeStart,
                TimeEnd = timeEnd,
            });
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult DeleteShift(int id)
        {
            db.Shifts.Remove(db.Shifts.FirstOrDefault(s => s.Id == id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
