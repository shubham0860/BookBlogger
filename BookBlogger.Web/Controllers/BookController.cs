using System.Web.Mvc;

namespace BookBlogger.Web.Controllers
{
    public class BookController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}