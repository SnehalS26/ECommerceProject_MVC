using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCrud_MVC.Models;

namespace ProductCrud_MVC.Controllers
{
    public class OrderController : Controller
    {
        IConfiguration configuration;       
        OrderCrud ordercrud;
        ProductCrud productcrud;
        public OrderController(IConfiguration configuration)
        {
            this.configuration = configuration;                  
            ordercrud = new OrderCrud(this.configuration);
            productcrud = new ProductCrud(this.configuration);
        }
        //[HttpGet]
        //public ActionResult Orders(int id)
        //{
        //    try
        //    {
        //        Orders order = new Orders();
        //        string orders = HttpContext.Session.GetString("uid");
        //        order.Uid = Convert.ToInt32(orders);
        //        order.Prodid = id;
        //        //order.Price = Convert.ToDecimal("price");
        //        order.Quantity = 1;
        //        int result = ordercrud.Order(order);
        //        if (result == 1)
        //        {
        //            return RedirectToAction(nameof(ConfirmOrders));
        //        }
        //        else
        //            return View();
        //    }
        //    catch(Exception ex)
        //    {
        //        return View();
        //    }
        //}

        [HttpGet]
        public ActionResult ConfirmOrders(int id)
        {
            var model = productcrud.GetProductById(id);
            //var model = ordercrud.GetOrderById(uid);
            return View(model);

        }
        [HttpGet]
        //[ActionName("ConfirmOrders")]
        public ActionResult PlaceOrder(int id)
        {
            try
            {
                string uid = HttpContext.Session.GetString("uid");
                Orders order = new Orders();
                order.Uid = Convert.ToInt32(uid);
                order.Prodid = id;
                //order.Price = Convert.ToDecimal("price");
                order.Quantity = 1;
                int result = ordercrud.AddOrder(order);
                if (result == 1)
                {
                    return RedirectToAction(nameof(OrderStatus));
                }
                else
                    return View();
            }
           catch(Exception ex) 
            {
                return View();
            }

        }
        public ActionResult OrderStatus()
        {
            return View();
        }
        public ActionResult ViewOrder()
        {
            string uid = HttpContext.Session.GetString("uid");
            var result = ordercrud.ViewOrder(Convert.ToInt32(uid));
            return View(result);
       }
        public ActionResult CancelOrder(int id)
        {

            try
            {
                var result = ordercrud.CancelOrder(id);
               
               return this.RedirectToAction("ProductList", "UserProduct");
            }
            catch (Exception ex)
            {
                return View();
            }

        }





    }
}
