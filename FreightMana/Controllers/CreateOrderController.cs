using Azure.Core;
using FreightMana.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace FreightMana.Controllers
{
    public class CreateOrderController : Controller
    {
        ManaFreightmentContext db = new ManaFreightmentContext();
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddOrder(
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
            String note
            )
        {
            Receiver receiver = new Receiver()
            {
                Name = receiverName,
                PhoneNumber = receiverPhone,
                Address = receiverAddress + " " + receiverPhuong + " " + receiverHuyen + " " + receiverTinh
            };
            db.Receivers.Add( receiver );
            db.SaveChanges();
          
            Sender sender = new Sender()
            {
                Name = senderName,
                PhoneNumber = senderPhone,
                Address = senderAddress + " " + senderPhuong + " " + senderHuyen + " " + senderTinh

            };
            Transport transport = db.Transports.FirstOrDefault(e=>e.Id== transportID);
            db.Senders.Add( sender );
            db.SaveChanges();
            Order order = new Order()
            {
                ReceiverId = receiver.Id,
                SenderId = sender.Id,
                TransportId = transportID,
                TransportFee = (int)transport.Cost*(float)Calculate(kg,length,width,height),
                Product = productName,
                NumberOfProduct = numberOfProduct,
                Cod = (float)cod,
                RecordAt = DateTime.Now,
                WarehouseId = 1,
                Status = "Chờ xác nhận",
                Note = note

            };
            db.Orders.Add(order );
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public double Calculate(
            double kg,
            double length,
            double width,
            double height)
        {
            double scale = 1;
            if (length==1) scale *= 1;
            else if (length==2) scale *= 1.2;
            else if (length == 3) scale *= 1.4;

            if (width == 1) scale *= 1;
            else if (width == 2) scale *= 1.2;
            else if (width == 3) scale *= 1.4;

            if (height == 1) scale *= 1;
            else if (height == 2) scale *= 1.2;
            else if (height == 3) scale *= 1.4;

            if (kg > 5) scale *= 1.5;
            else if (kg <= 5 && kg>3) scale *= 1.2;
            else scale *= 1;
            return scale;
        }
    }
}
