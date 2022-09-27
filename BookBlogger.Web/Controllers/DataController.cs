using BookBlogger.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Threading.Tasks;
//using System.Data.Objects;

namespace BookBlogger.Web.Controllers
{
    public class DataController : ApiController
    {
        private BooksBloggerEntities bookBloggerEntities = new BooksBloggerEntities();
        
        [HttpGet]
        public IHttpActionResult Get()
        {
            using (BooksBloggerEntities entities = new BooksBloggerEntities())
            {
                ObjectParameter responseMessage = new ObjectParameter("responseMessage", typeof(string));

                var book = entities.ReadBooks(responseMessage).ToList();

                return Json(book);
            }
        }


    }
}
