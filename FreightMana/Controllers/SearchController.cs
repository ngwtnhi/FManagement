using FreightMana.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreightMana.Controllers
{
    public class SearchController : Controller
    {
        ManaFreightmentContext db = new ManaFreightmentContext();
        public IActionResult Index()
        {
            
            return View();
        }
        public IActionResult Search(int orderID)
        {
            var orders = db.Orders.Where(o=>o.OrderId ==orderID).Include(o => o.Sender).ToList();
            return View("Index", orders);
        }
    }
}
