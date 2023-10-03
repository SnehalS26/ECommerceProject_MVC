using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCrud_MVC.Models;

namespace ProductCrud_MVC.Controllers
{
    public class UserProductController : Controller
    {
        IConfiguration configuration;
        ProductCrud prodcrud;
        CartCrud cartcrud;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment env;
        public UserProductController(IConfiguration configuration , Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.configuration = configuration;
            prodcrud = new ProductCrud(this.configuration);   
            cartcrud = new CartCrud(this.configuration);
            this.env = env;

        }
        // GET: UserProductController
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SinglePage(int id)
        {
            var model = prodcrud.GetProductById(id);
            return View(model);
        }
        // GET: UserProductController/Details/5
        public ActionResult ProductList(int pg=1)
        {
            var model = prodcrud.GetProducts();
            const int pagesize = 8;
            if (pg < 1)
            {
                pg = 1;
            }

            int recscount = model.Count();

            var pager = new Pager(recscount, pg, pagesize);

            int recskip = (pg - 1) * pagesize;

            var data = model.Skip(recskip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;
            return View(data);
        }

        // GET: UserProductController/Create
        [HttpGet]
        public ActionResult AddToCart(int id) //product id
        {
            try
            {
                Cart cart = new Cart();
                string userid = HttpContext.Session.GetString("uid");
                cart.Userid = Convert.ToInt32(userid);
                cart.Productid = id;
                cart.Quantity = 1;
                int result = cartcrud.AddToCart(cart);
                if(result == 1)
                {
                    return RedirectToAction(nameof(ViewCart));
                }
                else
                return View();
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        

        // GET: UserProductController/Edit/5
        public ActionResult ViewCart()
        {
            string userid = HttpContext.Session.GetString("uid");
            var model = cartcrud.ViewCart(Convert.ToInt32(userid));
            return View(model);
        }

       
        

        // GET: UserProductController/Delete/5
        public ActionResult RemoveCart(int id)
        {
            try
            {
                var result = cartcrud.DeleteCart(id);
                return RedirectToAction(nameof(ViewCart));
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        
        
    }
}
