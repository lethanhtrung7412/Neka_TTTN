using CosmeticsShop.Models;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CosmeticsShop.Controllers
{
    public class OrderManageController : Controller
    {
        ShoppingEntities db = new ShoppingEntities();
        public bool CheckRole(string type)
        {
            Models.User user = Session["User"] as Models.User;
            if (user != null && user.UserType.Name == type)
            {
                return true;
            }
            return false;
        }
        // GET: OrderManage
        public ActionResult Index(int? page = 1)
        {
            if (CheckRole("Admin"))
            {

            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            List<Order> orders = db.Orders.OrderBy(x=> x.ID).ToList();
            //Phân trang
            if (page == null) page = 1;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            PagedList<Order> models = new PagedList<Order>(orders.AsQueryable(), pageNumber, pageSize);

            //Trang hiện tại
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }
        public ActionResult Details(int ID)
        {
            if (CheckRole("Admin"))
            {

            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            ViewBag.IsProcessed = (db.Orders.Find(ID).Status == "Processing") ? false : true;
            List<OrderDetail> orderDetails = db.OrderDetails.Where(x => x.OrderID.Value == ID).ToList();
            return View(orderDetails);
        }
        public ActionResult Processed(int ID)
        {
            Order order = db.Orders.Find(ID);
            order.Status = "Processed";
            order.DateShip = DateTime.Now.AddDays(3);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delivering(int ID)
        {
            Order order = db.Orders.Find(ID);
            order.Status = "Delivering";
            order.DateShip = DateTime.Now.AddDays(3);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}