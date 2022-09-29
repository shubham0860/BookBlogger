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
            //return View();
            return View();

        }

        public ActionResult Login(UserLogin userLogin)
        {
            ViewBag.Message = "Your application description page.";

            if(userLogin != null)
            {
                //TODO Redirect To Single Page with these Login Info.

            }
                return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}