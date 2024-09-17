using FreightMana.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreightMana.Controllers
{
	public class DashboardController : Controller
    {
		ManaFreightmentContext db = new ManaFreightmentContext();
      //  [Authorize]
        public IActionResult Index()
        {
			var totalRevenue = db.Orders.Where(o => o.Status != "Chờ xác nhận").Sum(o => o.TransportFee);
			ViewBag.TotalRevenue = totalRevenue;

			// Tính tổng số đơn hàng
			int totalOrders = db.Orders.Where(o => o.Status != "Chờ xác nhận").Count();
			ViewBag.TotalOrders = totalOrders;

			// Tính tổng số đơn hàng đã giao
			int deliveredOrders = db.Orders.Count(o => o.Status == "Đã hoàn thành");
			ViewBag.DeliveredOrders = deliveredOrders;

			// Tính tổng số đơn hàng đã hủy
			int cancelledOrders = db.Orders.Count(o => o.Status == "Đã hủy");
			ViewBag.CancelledOrders = cancelledOrders;

			// Tính tổng số đơn hàng chưa giao
			int pendingOrders = db.Orders.Count(o => o.Status != "Đã hoàn thành" && o.Status != "Đã hủy" && o.Status != "Chờ xác nhận");
			ViewBag.PendingOrders = pendingOrders;

            // list all
            var allOrders = db.Orders
           .Select(o => new
           {
               o.Receiver.Name,
               o.Receiver.Address,
               o.Receiver.PhoneNumber,
               o.OrderId,
               o.Cod,
               o.Transport.Cost,
               o.Status,
               o.ConfirmAt
           })
           .Where(o=>o.Status!="Chờ xác nhận")
           .ToList();
            ViewBag.AllOrders = allOrders;

            // list đơn chưa giao
            var shippingOrders = db.Orders
              .Where(o=> o.Status == "Đang giao hàng")
              .Select(o => new
              {
                  o.Receiver.Name,
                  o.Receiver.Address,
                  o.Receiver.PhoneNumber,
                  o.OrderId,
                  o.Cod,
                  o.Transport.Cost,
                  o.Status,
                  o.ConfirmAt
              })
              .ToList();
            ViewBag.ShippingOrders = shippingOrders;

            //list đơn hoàn thành
            var completedOrders = db.Orders
              .Where(o => o.Status == "Đã hoàn thành")
              .Select(o => new
              {
                  o.Receiver.Name,
                  o.Receiver.Address,
                  o.Receiver.PhoneNumber,
                  o.OrderId,
                  o.Cod,
                  o.Transport.Cost,
                  o.Status,
                  o.CompleteAt
              })
              .ToList();
            ViewBag.CompletedOrders = completedOrders;

            //list đơn đã hủy
            var cancelOrders = db.Orders
              .Where(o => o.Status == "Đã hủy")
              .Select(o => new
              {
                  o.Receiver.Name,
                  o.Receiver.Address,
                  o.Receiver.PhoneNumber,
                  o.OrderId,
                  o.Cod,
                  o.Transport.Cost,
                  o.Status,
                  o.CancelAt
              })
              .ToList();
            ViewBag.CancelOrders = cancelOrders;
            return View();
        }
    }
}
