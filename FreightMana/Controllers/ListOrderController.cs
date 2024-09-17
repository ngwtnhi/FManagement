using FreightMana.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreightMana.Controllers
{
    public class ListOrderController : Controller
    {
        ManaFreightmentContext db = new ManaFreightmentContext();
        private static List <Order> list;
        public IActionResult Index()
        {
            list = db.Orders
           .Where(o => o.Status != "Chờ xác nhận")
           .Include(o => o.Receiver)
           .Include(o => o.Transport)
           .ToList();
            // System.Diagnostics.Debug.WriteLine(orders[0].PhoneNumber);
            return View(list);
        }
        public ActionResult SaveStatus(List<Order> orders)
        {
     
            for(int i = 0;i< orders.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine(list[i].OrderId);
                var order = db.Orders.Find(list[i].OrderId);
                order.Status = orders[i].Status;
                if(order.Status == "Đã hủy") order.CancelAt = DateTime.Now;
                if (order.Status == "Đã hoàn thành") order.CompleteAt = DateTime.Now;
                if (order.Status == "Đã nhập kho") order.ConfirmAt = DateTime.Now;
            }
            db.SaveChanges();

            
            return RedirectToAction("Index");
        }
    
        [HttpPost]
        public IActionResult Search(string keyword)
        {
            System.Diagnostics.Debug.WriteLine("searching");
            list = db.Orders.Where(o =>
                (o.OrderId.ToString().Contains(keyword) ||
                o.Receiver.Name.Contains(keyword) ||
                o.Receiver.PhoneNumber.Contains(keyword) ||
                o.Receiver.Address.Contains(keyword) ||
                o.Status.Contains(keyword)) &&
                o.Status != "Chờ xác nhận"
            )
                .Include(o => o.Receiver)
                .Include(o => o.Transport)
                .ToList();

            return View("Index", list);
        }
        public IActionResult EditOrder(int orderId)
        {
            var order = db.Orders.Include(o => o.Receiver).Include(o => o.Sender)
                .Include(o => o.Transport).Where(o=> o.OrderId == orderId).First();
            return View(order);
        }

        [HttpPost]
        public IActionResult ConfirmEdit(Order order)
        {
            var o = db.Orders.Include(o => o.Receiver).Include(o => o.Sender)
                .Include(o => o.Transport).Where(o => o.OrderId == order.OrderId).First();
            var sender = db.Senders.Find(order.SenderId);
            sender.Name = order.Sender.Name;
            sender.Address = o.Sender.Address;
            var receiver = db.Receivers.Find(order.ReceiverId);
            receiver.Address = o.Receiver.Address;
            receiver.Name = order.Receiver.Name;    
            receiver.PhoneNumber = order.Receiver.PhoneNumber; 
            db.SaveChanges();
            o.Cod = order.Cod;
            o.TransportId= order.TransportId;
            o.Note = order.Note;
            o.Product = order.Product;
            o.NumberOfProduct= order.NumberOfProduct;
            db.SaveChanges();
            
            return RedirectToAction("Index");
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
