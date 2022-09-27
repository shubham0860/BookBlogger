using BookBlogger.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BookBlogger.Web.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        //HttpClient httpClient = new HttpClient();

        //public async Task<ActionResult> Index()
        //{
        //    List<ReadBooks_Result> list = new List<ReadBooks_Result>();
        //    httpClient.BaseAddress = new Uri("https://localhost:44367/");
        //    HttpResponseMessage message = await httpClient.GetAsync("api/data/get");
        //    if (message.IsSuccessStatusCode)
        //    {
        //        var display = message.Content.ReadAsAsync<List<ReadBooks_Result>>();
        //        list = display.Result;
        //    }
        //    return View(list);
        //}
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}