﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Genealogie.ASP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return RedirectToAction("About");
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return RedirectToAction("Contact");
            return View();
        }
    }
}