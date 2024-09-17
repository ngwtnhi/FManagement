using FreightMana.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreightMana.Controllers
{
    public class ConfirmController : Controller
    {
        ManaFreightmentContext db = new ManaFreightmentContext();
        private static List<Order> orders;
        public IActionResult Index()
        {
            orders = new List<Order>() { };
            orders = db.Orders
            .Where(o => o.Status == "Chờ xác nhận")
            .Include(o => o.Receiver)
            .Include(o => o.Transport)
            .ToList();
            
            return View(orders);
        }
        [HttpPost]
        public IActionResult Confirm(List<String> statusList)
        {
           // List<Order> list = db.Orders.Where(o => o.Status == "Chờ xác nhận"  ).ToList();
            if(orders.Count == statusList.Count)
            {
                for (int i = 0; i < orders.Count; i++)
                {
                    var order = db.Orders.Find(orders[i].OrderId);
                   order.Status = statusList[i];
                    order.ConfirmAt = DateTime.Now;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Search(string keyword)
        {
            System.Diagnostics.Debug.WriteLine("searching");
            orders = db.Orders.Where(o =>
                (o.OrderId.ToString().Contains(keyword) ||
                o.Receiver.Name.Contains(keyword) ||
                o.Receiver.PhoneNumber.Contains(keyword) ||
                o.Receiver.Address.Contains(keyword) ||
                o.Status.Contains(keyword)) &&
                o.Status == "Chờ xác nhận"
            )
                .Include(o => o.Receiver)
                .Include(o => o.Transport)
                .ToList();

            return View("Index", orders);
        }

        public IActionResult DeleteOrder(int orderId)
        {
            var order = db.Orders.Find(orderId);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
