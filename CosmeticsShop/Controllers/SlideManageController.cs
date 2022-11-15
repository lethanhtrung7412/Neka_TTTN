//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using CosmeticsShop.Models;

//namespace CosmeticsShop.Controllers
//{
//    public class SlideManageController : Controller
//    {
//        private ShoppingEntities db = new ShoppingEntities();
//        public bool CheckRole(string type)
//        {
//            Models.User user = Session["User"] as Models.User;
//            if (user != null && user.UserType.Name == type)
//            {
//                return true;
//            }
//            return false;
//        }
//        // GET: TestManage
//        public ActionResult Index()
//        {
//            if (CheckRole("Admin"))
//            {

//            }
//            else
//            {
//                return RedirectToAction("Index", "Admin");
//            }
//            var slides = db.Slides.Include(s => s.User);
//            return View(slides.ToList());
//        }
//        public ActionResult ToggleActive(int ID)
//        {
//            Slide slide = db.Slides.Find(ID);
//            slide.Status = !slide.Status;
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }
//        // GET: TestManage/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Slide slide = db.Slides.Find(id);
//            if (slide == null)
//            {
//                return HttpNotFound();
//            }
//            return View(slide);
//        }

//        // GET: TestManage/Create
//        public ActionResult Create()
//        {
//            ViewBag.CreatedBy = new SelectList(db.Users, "ID", "Name");
//            return View();
//        }

//        [HttpPost]     
//        public ActionResult Create(Slide slide, HttpPostedFileBase[] ImageUpload)
//        {
//            for (int i = 0; i < ImageUpload.Length; i++)
//            {
//                //Check content image
//                if (ImageUpload[i] != null && ImageUpload[i].ContentLength > 0)
//                {
//                    //Get file name
//                    var fileName = Path.GetFileName(ImageUpload[i].FileName);
//                    //Get path
//                    var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);
//                    //Check exitst
//                    if (!System.IO.File.Exists(path))
//                    {
//                        //Add image into folder
//                        ImageUpload[i].SaveAs(path);
//                    }
//                }
//            }

//            if (ImageUpload[0] != null)
//            {
//                slide.Image = ImageUpload[0].FileName;
//            }
//            slide.ID = 0;
//            slide.CreatedBy = (Session["User"] as Models.User).ID;
//            slide.DisplayOrder = 0;
//            slide.Status = true;
//            slide.Description = "";
//            slide.CreatedDate = DateTime.Now;
//            db.Slides.Add(slide);
//            db.SaveChanges();

//            ViewBag.Message = "Thêm thành công";
//            return View("Details", slide);
//        }

//        public ActionResult Details(int ID)
//        {
//            if (CheckRole("Admin"))
//            {

//            }
//            else
//            {
//                return RedirectToAction("Index", "Admin");
//            }
//            Product product = db.Products.Find(ID);
//            ViewBag.CategoryList = db.Categories.ToList();
//            return View(product);
//        }

//        [HttpPost]
//        public ActionResult Edit(Slide slide, HttpPostedFileBase[] ImageUpload)
//        {
//            Slide slideUpdate = db.Slides.Find(slide.ID);
//            slideUpdate.Name = slide.Name;
//            slideUpdate.DisplayOrder = slide.DisplayOrder;
//            slideUpdate.Status = slide.Status;
//            slideUpdate.Description = slide.Description;

//            for (int i = 0; i < ImageUpload.Length; i++)
//            {
//                //Check content image
//                if (ImageUpload[i] != null && ImageUpload[i].ContentLength > 0)
//                {
//                    //Get file name
//                    var fileName = Path.GetFileName(ImageUpload[i].FileName);
//                    //Get path
//                    var path = Path.Combine(Server.MapPath("~/Content/images"), fileName);
//                    //Check exitst
//                    if (!System.IO.File.Exists(path))
//                    {
//                        //Add image into folder
//                        ImageUpload[i].SaveAs(path);
//                    }
//                }
//            }

//            if (ImageUpload[0] != null)
//            {
//                slideUpdate.Image = ImageUpload[0].FileName;
//            }

//            db.SaveChanges();

//            ViewBag.Message = "Cập nhật thành công";
//            return View("Details", slideUpdate);
//        }

//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Slide slide = db.Slides.Find(id);
//            if (slide == null)
//            {
//                return HttpNotFound();
//            }
//            return View(slide);
//        }

//        [HttpPost]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            Slide slide = db.Slides.Find(id);
//            db.Slides.Remove(slide);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
