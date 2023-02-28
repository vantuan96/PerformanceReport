﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestSSO.Models;

namespace TestSSO.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        
        public ActionResult Index()
        {
          
            var routeData = HttpContext.Request.RequestContext.RouteData;
            var LoginTokenCookie = HttpContext.Request.Cookies["VmSigninToken"];
            if (LoginTokenCookie != null)
            {
                var model = new Student();
                model.name = "Tuấn";
                return View(model);
            }
            return View();
          
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}