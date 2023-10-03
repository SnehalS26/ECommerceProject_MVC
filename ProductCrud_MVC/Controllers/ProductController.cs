using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;
using ProductCrud_MVC.Models;
using System.Data;

namespace ProductCrud_MVC.Controllers
{
    public class ProductController : Controller
    {
        IConfiguration configuration;
        CategoryCrud catcrud;
        ProductCrud prodcrud;
        UsersCrud usercrud;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment env;
        
        public ProductController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.configuration = configuration;
            catcrud = new CategoryCrud(this.configuration);
            prodcrud = new ProductCrud(this.configuration);
            usercrud = new UsersCrud(this.configuration);
            this.env = env;
            

        }   
             

        // GET: ProductController
        public ActionResult Index()
        {
            return View(prodcrud.GetProducts());
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View(prodcrud.GetProductById(id));
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            ViewBag.Categories = catcrud.GetCategory();
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product prod , IFormFile file)
        {
            try
            {
                using (var fs = new FileStream(env.WebRootPath + "\\images\\" + file.FileName, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fs);
                }
                prod.Imageurl = "~/images/" + file.FileName;
                var result = prodcrud.AddProduct(prod);
                if (result >= 1)
                    return RedirectToAction(nameof(Index));
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var prod = prodcrud.GetProductById(id);
            ViewBag.Categories = catcrud.GetCategory();
            HttpContext.Session.SetString("oldImageUrl", prod.Imageurl);
            return View(prod);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product prod, IFormFile file)
        {
            try
            {
                string oldimageurl = HttpContext.Session.GetString("oldImageUrl");
                if (file != null)
                {
                    using (var fs = new FileStream(env.WebRootPath + "\\images\\" + file.FileName, FileMode.Create, FileAccess.Write))
                    {
                        file.CopyTo(fs);
                    }
                    prod.Imageurl = "~/images/" + file.FileName;


                    string[] str = oldimageurl.Split("/");
                    string str1 = (str[str.Length - 1]);
                    string path = env.WebRootPath + "\\images\\" + str1;
                    System.IO.File.Delete(path);
                }
                else
                {
                    prod.Imageurl = oldimageurl;
                }
                var result = prodcrud.UpdateProduct(prod);
                if (result >= 1)
                    return RedirectToAction(nameof(Index));
                else
                {
                    return View();
                }
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(prodcrud.GetProductById(id));
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteCinfirm(int id)
        {
            try
            {
                var prod = prodcrud.GetProductById(id);
                string[] str = prod.Imageurl.Split("/");
                string str1 = (str[str.Length - 1]);
                string path = env.WebRootPath + "\\images\\" + str1;
                System.IO.File.Delete(path);
                var result = prodcrud.DeleteProduct(id);
                if (result >= 1)
                    return RedirectToAction(nameof(Index));
                else
                {
                    return View();
                }
            }
            catch(Exception ex)
            {
                return View();
            }
        }
        // GET: UsersController/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: UsersController/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Users users)
        {
            try
            {
                var result = usercrud.AddUsers(users);
                if (result >= 1)
                    return RedirectToAction(nameof(Login));
                else 
                return View();
                
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Login/5
        public ActionResult Login(int id)
        {
            return View();
        }

        // POST: UsersController/Login/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users user)
        {
            try
            {
                
                var model = usercrud.GetEmailPassword(user.Email, user.Password);
                if(model.Uid > 0)
                {
                    HttpContext.Session.SetString("roleid", model.Roleid.ToString());
                    HttpContext.Session.SetString("uid", model.Uid.ToString());
                    HttpContext.Session.SetString("email", model.Email);
                    if (model.Roleid == 1)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else if (model.Roleid == 2)
                    {
                        return this.RedirectToAction("ProductList", "UserProduct");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Invalid Email and Password.";
                        return View();
                    }

                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid Email and Password.";
                    return View();
                }

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Invalid Email and Password.";
                return View();
            }
        }
        // GET: UsersController/Logout/5
        public ActionResult Logout(int id)
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }

        
    }
}
