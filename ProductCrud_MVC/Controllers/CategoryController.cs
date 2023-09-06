using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCrud_MVC.Models;

namespace ProductCrud_MVC.Controllers
{
    public class CategoryController : Controller
    {
        IConfiguration configuration;
        private CategoryCrud catcrud;
        public CategoryController(IConfiguration configuration)
        {
            this.configuration = configuration;
            catcrud = new CategoryCrud(this.configuration);
        }
        // GET: CategoryController
        public ActionResult Index()
        {
            return View(catcrud.GetCategory());
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int cid)
        {
            return View(catcrud.GetCategoryById(cid));
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category cat)
        {
            try
            {
                var res = catcrud.AddCategory(cat);
                if(res== 1)
                return RedirectToAction(nameof(Index));
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int cid)
        {
            return View(catcrud.GetCategoryById(cid));
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category cat)
        {
            try
            {
                var res = catcrud.UpdateCategory(cat);
                if (res == 1)
                    return RedirectToAction(nameof(Index));
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int cid)
        {
            return View(catcrud.GetCategoryById(cid));
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(Category cat)
        {
            try
            {
                var res = catcrud.DeleteCategory(cat);
                if (res == 1)
                    return RedirectToAction(nameof(Index));
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
