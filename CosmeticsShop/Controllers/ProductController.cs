using CosmeticsShop.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CosmeticsShop.Controllers
{
    public class ProductController : Controller
    {
        ShoppingEntities db = new ShoppingEntities();
        // GET: Product
        public ActionResult Index(int CategoryID = 0, string keyword = "", int? page = 1)
        {
            ViewBag.ListCategory = db.Categories.Where(x => x.IsActive == true).ToList();
            if (keyword != "")
            {
                ViewBag.NamePage = "Search product";
                ViewBag.ListProduct = db.Products.Where(x => x.IsActive == true && x.Name.Contains(keyword)).ToList();
                return View();
            }
            if (CategoryID != 0)
            {
                ViewBag.NamePage = "Category " + db.Categories.Find(CategoryID).Name;
                ViewBag.ListProduct = db.Products.Where(x => x.IsActive == true && x.CategoryID == CategoryID).ToList();
            }
            else
            {
                ViewBag.NamePage = "All products";
                ViewBag.ListProduct = db.Products.Where(x => x.IsActive == true).ToList();
            }
            return View();
        }
        public ActionResult Details(int ID)
        {
            Product product = db.Products.Find(ID);
            return View(product);
        }
        public ActionResult RemoveProduct(int ID)
        {
            Product product = db.Products.Find(ID);
            db.Products.Remove(product);
            return View(product);
        }
    }
}