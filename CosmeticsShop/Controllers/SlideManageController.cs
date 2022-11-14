using CosmeticsShop.Models;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CosmeticsShop.Controllers
{
    public class SlideManageController : Controller
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
        // GET: ProductManage
        public ActionResult Index()
        {
            if (CheckRole("Admin"))
            {

            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        public ActionResult Add()
        {
            if (CheckRole("Admin"))
            {

            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Add(Slide slide, HttpPostedFileBase[] ImageUpload)
        {
            for (int i = 0; i < ImageUpload.Length; i++)
            {
                //Check content image
                if (ImageUpload[i] != null && ImageUpload[i].ContentLength > 0)
                {
                    //Get file name
                    var fileName = Path.GetFileName(ImageUpload[i].FileName);
                    //Get path
                    var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);
                    //Check exitst
                    if (!System.IO.File.Exists(path))
                    {
                        //Add image into folder
                        ImageUpload[i].SaveAs(path);
                    }
                }
            }

            if (ImageUpload[0] != null)
            {
                slide.Image = ImageUpload[0].FileName;
            }

            slide.CreatedBy = (Session["User"] as Models.User).ID;
            slide.DisplayOrder = "";
            slide.Status = true;
            slide.Description = "";
            slide.CreatedDate = DateTime.Now;
            db.Slides.Add(slide);
            db.SaveChanges();

            ViewBag.Message = "Thêm thành công";
            return View("Details", slide);
        }
    }
}