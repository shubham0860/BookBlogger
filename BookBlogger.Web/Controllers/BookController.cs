using BookBlogger.Data;
using System.Web.Mvc;

namespace BookBlogger.Web.Controllers
{
    public class BookController : Controller
    {
        //public BooksBloggerEntities db = new BooksBloggerEntities();
        public ActionResult Index()
        {
            //var Username = db.Users.Find(AccountController.UserId).Username;
            return View();
        }

    }
}