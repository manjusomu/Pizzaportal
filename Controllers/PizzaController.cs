using Microsoft.AspNetCore.Mvc;
using PizzaOrderPortal.Models;

namespace PizzaOrderPortal.Controllers
{
   
    public class PizzaController : Controller
    {
        static public List<Pizza> pizzadetails = new List<Pizza>() {

          new Pizza { PizzaId = 100,Type = "Normal Pizza", Price =30},
          new Pizza { PizzaId = 101,Type = "Chicken Pizza",Price=50},
          new Pizza { PizzaId = 102,Type = "Spice lip Pizza ",Price=10},
          new Pizza { PizzaId = 103,Type = "Mottan Pizaa ",Price=100},
          new Pizza { PizzaId = 104,Type = "Manjury Pizza",Price=110},
          new Pizza { PizzaId = 105,Type = "Special Super Pizza",Price=120},
          new Pizza { PizzaId = 106,Type = "Rolled Pizza",Price=55}
    };
        static public List<OrderInfo> orderdetails = new List<OrderInfo>();
        public IActionResult Index()
        {
            return View(pizzadetails);
        }
        public IActionResult Cart(int id)
        {
            var found = (pizzadetails.Find(p => p.PizzaId == id));

            TempData["id"] = id;

            return View(found);

        }
        [HttpPost]
        public IActionResult Cart(IFormCollection f)
        {
            Random r = new Random();
            int id = Convert.ToInt32(TempData["id"]);
            OrderInfo o = new OrderInfo();
            var found = (pizzadetails.Find(p => p.PizzaId == id));
            o.OrderId = r.Next(100, 999);
            o.PizzaId = id;
            o.Price = found.Price;
            o.Type = found.Type;
            o.Quantity = Convert.ToInt32(Request.Form["qty"]);
            o.TotalPrice = o.Price * o.Quantity;

            orderdetails.Add(o);

            return RedirectToAction("Checkout");

        }


        public IActionResult Checkout()
        {

            //var found = orderdetails.Find(p => p.OrderId == orderid);

            //Console.WriteLine(orderdetails); 
            return View(orderdetails);

        }
    }
}

