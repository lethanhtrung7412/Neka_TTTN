﻿using CosmeticsShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CosmeticsShop.Controllers
{
    public class SlideController : Controller
    {
        // GET: Slide
        ShoppingEntities db = new ShoppingEntities();
        public ActionResult Index()
        {
            var news = db.Slides.AsNoTracking().OrderBy(x => x.DisplayOrder);
            return View(news);
        }
        public ActionResult Details(int ID)
        {
            Slide slide = db.Slides.Find(ID);
            return View(slide);
        }
    }
}