using FreightMana.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Text.Json;
using System.Text;
using System.Collections.Generic;

namespace FreightMana.Controllers
{
    [Route("excel")]
    public class CreateGoodsController : Controller
    {
        private static List<Order> listOrder = new List<Order>();

        ManaFreightmentContext db = new ManaFreightmentContext();
        public IActionResult Index()
        {
            System.Diagnostics.Debug.WriteLine(listOrder.Count);
            return View( listOrder);
        }
        [HttpPost("upload")]
        public IActionResult ReadExcelFile(IFormFile excelFile)
        {
            //List <Order> listOrder = new List<Order>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            if (excelFile != null && excelFile.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    excelFile.CopyTo(stream);
                    using (var package = new ExcelPackage(stream)) //Tạo đối tượng chứa dữ liệu của file excel
                    {
                        var worksheet = package.Workbook.Worksheets[0]; //Lấy ra sheet đầu tiên
                        var rowCount = worksheet.Dimension.Rows; //Tính toán số dòng

                        for (int row = 3; row <= rowCount; row++) //Duyệt từng dòng và lấy dữ liệu
                        {
                            try { 
                            //Tên sp 
                                var productName = worksheet.Cells[row, 1].Value?.ToString();
                                //Số lượng
                                var numOfProduct = int.Parse(worksheet.Cells[row, 2].Value?.ToString());
                                //Mã vận chuyển
                                var transportId = int.Parse(worksheet.Cells[row, 3].Value?.ToString());
                                //COD
                                var cod = float.Parse(worksheet.Cells[row, 4].Value?.ToString());
                                //Phí vận chuyển
                                var fee = float.Parse(worksheet.Cells[row, 5].Value?.ToString());
                                //Note
                                var note = worksheet.Cells[row, 6].Value?.ToString();
                                //Tên người gửi
                                var senderName = worksheet.Cells[row, 7].Value?.ToString();
                                //Số điện thoại người gửi
                                var senderPhone = worksheet.Cells[row, 8].Value?.ToString();
                                //Địa chỉ người gửi
                                var senderAddress = worksheet.Cells[row, 9].Value?.ToString();
                                //Tên người nhận
                                var recName = worksheet.Cells[row, 10].Value?.ToString();
                                //Số điện thoại người nhận
                                var recPhone = worksheet.Cells[row, 11].Value?.ToString();
                                //Địa chỉ người nhận
                                var recAddress = worksheet.Cells[row, 12].Value?.ToString();
                                //Thêm người gửi
                                Sender sender = new Sender() { 
                                    Name = senderName,
                                    PhoneNumber = senderPhone,
                                    Address = senderAddress,
                                };
                                
                                //db.Senders.RemoveRange(db.Senders.Where(r => r.Name == sender.Name));
                                db.Senders.Add(sender);
                                //db.SaveChanges();
                                //Thêm người nhận
                                Receiver receiver = new Receiver()
                                {
                                    Name = recName,
                                    PhoneNumber = recPhone,
                                    Address = recAddress
                                };

                               
                                //db.Receivers.RemoveRange(db.Receivers.Where(r => r.Name == receiver.Name));
                                db.Receivers.Add(receiver);
                                db.SaveChanges();
                                //System.Diagnostics.Debug.WriteLine(receiver.Address);
                                //Thêm đơn hàng
                                Order order = new Order()
                                {
                                    Product = productName,
                                    NumberOfProduct = numOfProduct,
                                    TransportId = transportId,
                                    Cod = cod,
                                    TransportFee = fee,
                                    Note = note,
                                    SenderId = sender.Id,
                                    ReceiverId = receiver.Id,
                                    RecordAt = DateTime.Now,
                                    WarehouseId = 1,
                                    Status = "Chờ xác nhận"
                                };
                                //System.Diagnostics.Debug.WriteLine(order.SenderId);
                                //db.Orders.RemoveRange(db.Orders.Where(o => o.Product == order.Product));                              
                                db.Orders.Add(order);
                                db.SaveChanges();
                                listOrder.Add(order);
                                TempData["message"] = "Thêm đơn thành công";
                            }
                            catch (Exception e)
                            {
                                TempData["message"] = e.ToString();
                                return RedirectToAction("Index");
                            }
                        }
                    }
                }
              
            }
           
            return RedirectToAction("Index");
        }
    }
}
