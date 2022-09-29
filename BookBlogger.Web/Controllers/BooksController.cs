using System.Net;
using System.Net.Http;
using System.Web.Http;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using BookBlogger.Data;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace BookBlogger.Web.Controllers
{
    public class BooksController : ApiController
    {
        public BooksBloggerEntities db = new BooksBloggerEntities();

        public DataSourceResult GetBooks([System.Web.Http.ModelBinding.ModelBinder(typeof(WebApiDataSourceRequestModelBinder))]DataSourceRequest request)
        {
            ObjectParameter responseMessage = new ObjectParameter("responseMessage", typeof(string));

            var book = db.ReadBooks(responseMessage).ToList();
            return book.ToDataSourceResult(request);
        }

        public Book GetBook(int id)
        {
            Book book = db.Books.Find(id);
            if(book == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return book;
        }




        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}