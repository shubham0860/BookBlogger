using BookBlogger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookBlogger.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login(UserLogin userLogin)
        {
            ViewBag.Message = "Please Login";
                return View();
        }
    }
}