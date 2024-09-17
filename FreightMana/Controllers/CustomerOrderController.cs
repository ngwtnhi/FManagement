using FreightMana.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
namespace FreightMana.Controllers
{
    public class CustomerOrderController : Controller
    {
        ManaFreightmentContext db = new ManaFreightmentContext();
        public IActionResult Index()
        {
            // Lấy id của người dùng từ Session
            var userId = HttpContext.Session.GetInt32("UserId");

            // Kiểm tra nếu không có id hoặc người dùng không đăng nhập
            if (userId == null)
            {
                // Xử lý khi người dùng chưa đăng nhập, ví dụ: chuyển hướng đến trang đăng nhập
                return RedirectToAction("Index", "LoginCustomer");
            }

            // Truy vấn cơ sở dữ liệu để lấy thông tin người dùng từ id
            var user = db.CusAccounts.Find(userId);

            // Kiểm tra nếu không tìm thấy người dùng hoặc thông tin người dùng không hợp lệ
            if (user == null)
            {
                // Xử lý khi không tìm thấy thông tin người dùng, ví dụ: hiển thị thông báo lỗi
                ViewBag.ErrorMessage = "Không tìm thấy thông tin người dùng!";
            }
            else
            {
                // Lấy tên người dùng từ thông tin người dùng
                var userName = user.Username;

                // Truyền tên người dùng sang view để hiển thị
                ViewBag.UserName = userName;
            }

            return View();
        }

        [HttpPost]
        public IActionResult AddOrderCus(
            String senderName, 
            String senderPhone,
            String senderTinh,
            String senderHuyen,
            String senderPhuong,
            String senderAddress,
            String receiverName,
            String receiverPhone,
            String receiverTinh,
            String receiverHuyen,
            String receiverPhuong,
            String receiverAddress,
            String productName,
            int numberOfProduct,
            double kg,
            double length,
            double width,
            double height,
            int transportID,
            double cod,
            String note)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            Receiver receiver = new Receiver()
            {
                Name = receiverName,
                PhoneNumber = receiverPhone,
                Address = receiverAddress + " " + receiverPhuong + " " + receiverHuyen + " " + receiverTinh
            };
            db.Receivers.Add(receiver);
            db.SaveChanges();

            Sender sender = new Sender()
            {
                Name = senderName,
                PhoneNumber = senderPhone,
                Address = senderAddress + " " + senderPhuong + " " + senderHuyen + " " + senderTinh

            };
            Transport transport = db.Transports.FirstOrDefault(e => e.Id == transportID);
            db.Senders.Add(sender);
            db.SaveChanges();
            Order order = new Order()
            {   
                ReceiverId = receiver.Id,
                SenderId = sender.Id,
                TransportId = transportID,
                TransportFee = transport.Cost,
                Product = productName,
                NumberOfProduct = numberOfProduct,
                Cod = (float)cod,
                RecordAt = DateTime.Now,
                WarehouseId = 1,
                Status = "Chờ xác nhận",
                CusId = userId,
                Note = note

            };
            db.Orders.Add(order);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
