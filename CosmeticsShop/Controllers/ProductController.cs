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
            List<Product> products = new List<Product>();
            if (keyword != "")
            {
                ViewBag.NamePage = "Tìm kiếm sản phẩm";
                products = db.Products.Where(x => x.IsActive == true && x.Name.Contains(keyword)).ToList();
                return View();
            }
            if (CategoryID != 0)
            {
                ViewBag.NamePage = "Danh mục " + db.Categories.Find(CategoryID).Name;
                products = db.Products.Where(x => x.IsActive == true && x.CategoryID == CategoryID).ToList();
            }
            else
            {
                ViewBag.NamePage = "Tất cả sản phẩm";
                products = db.Products.Where(x => x.IsActive == true).ToList();
            }
           
            //Phân trang
            if (page == null) page = 1;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            PagedList<Product> models = new PagedList<Product>(products.AsQueryable(), pageNumber, pageSize);

            //Trang hiện tại
            ViewBag.ListProduct = products;
            ViewBag.CurrentPage = pageNumber;
            return View(models);
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