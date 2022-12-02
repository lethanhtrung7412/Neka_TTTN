using CosmeticsShop.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Tokenizer.Symbols;

namespace CosmeticsShop.Controllers
{
    public class ProductController : Controller
    {
        ShoppingEntities db = new ShoppingEntities();
        // GET: Product
        public ActionResult Index(int CategoryID = 0, string keyword = "", int SortPrice = 0)
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
                return View();
            }
            if (SortPrice == 1)
            {
                ViewBag.NamePage = "Lower to Higher Price";
                ViewBag.ListProduct = db.Products.Where(x => x.IsActive == true).OrderBy(x => x.Price).ToList();
                return View();
            }
            if (SortPrice == 2)
            {
                ViewBag.NamePage = "Higher to Lower Price";
                ViewBag.ListProduct = db.Products.Where(x => x.IsActive == true).OrderByDescending(x => x.Price).ToList();
            }
            else
            {
                ViewBag.NamePage = "All products";
                ViewBag.ListProduct = db.Products.Where(x => x.IsActive == true).ToList();
            }
            return View();
        }

        //public ActionResult Sort(int SortLower = 0)
        //{
        //    if (SortLower == 1)
        //    {
        //        ViewBag.NamePage = "Lower to Higher Price";
        //        ViewBag.ListProduct = db.Products.Where(x => x.IsActive == true).OrderBy(x => x.Price).ToList();
        //    }
        //    if (SortLower == 2)
        //    {
        //        ViewBag.NamePage = "Higher to Lower Price";
        //        ViewBag.ListProduct = db.Products.Where(x => x.IsActive == true).OrderByDescending(x => x.Price).ToList();
        //    }
        //    return RedirectToAction("Index");
        //}
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