using System.Net;
using System.Net.Http;
using System.Web.Http;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using BookBlogger.Data;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Http.ModelBinding;
using BookBlogger.Web.Models;
using System;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using NLog;

namespace BookBlogger.Web.Controllers
{
    public class BooksController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public BooksBloggerEntities db = new BooksBloggerEntities();

        public DataSourceResult GetBooks([ModelBinder(typeof(WebApiDataSourceRequestModelBinder))]DataSourceRequest request)
        {
            ObjectParameter responseMessage = new ObjectParameter("responseMessage", typeof(string));
            
           // try
            //{
                var book = db.ReadBooks(responseMessage).ToList();
                return book.ToDataSourceResult(request);
            //}
            //catch(Exception e)
            //{
                //logger.Error(e);
               // logger.Error(responseMessage.Value);
                //return book.ToDataSourceResult(request);
           // }

            //return book.ToDataSourceResult(request);

            //Debug.WriteLine(responseMessage.Value);

        }

        //[Route("api/books/GetUsername")]
        //[HttpGet]
        //public string GetUsername()
        //{
        //    return db.Users.Find(AccountController.UserId).Username;
        //}

        [Route("api/books/UserBooks")]
        [HttpGet]
        public DataSourceResult UserBooks([ModelBinder(typeof(WebApiDataSourceRequestModelBinder))] DataSourceRequest request)
        {
            ObjectParameter responseMessage = new ObjectParameter("responseMessage", typeof(string));

            var book = db.ReadBook(AccountController.UserId, responseMessage).ToList();

            return book.ToDataSourceResult(request);
        }

        [Route("api/books/GetAudit")]
        [HttpGet]
        public DataSourceResult GetAudits([ModelBinder(typeof(WebApiDataSourceRequestModelBinder))] DataSourceRequest request)
        {
            ObjectParameter responseMessage = new ObjectParameter("responseMessage", typeof(string));
            var AuditData = db.ReadAudit(responseMessage).ToList();

            return AuditData.ToDataSourceResult(request);
        }

        public HttpResponseMessage PostBook(Books books)
        {
            if (ModelState.IsValid)
            {
                //if(books.ISBN != null && books.BookName != null && books.Price != null && books.Details != null && books.AuthorName != null && books.Surname != null)
                //  {
                try
                {
                    ObjectParameter responseMessage = new ObjectParameter("responseMessage", typeof(string));

                    db.AddBook(AccountController.UserId, books.ISBN, books.BookName, books.Price, books.Details, books.ImageUrl, books.DownloadUrl, books.AuthorName, books.Surname, responseMessage);
                    db.SaveChanges();

                    DataSourceResult result = new DataSourceResult
                    {
                        Data = new[] { books },
                        Total = 1
                    };


                    var Id = (from record in db.Books orderby record.ID descending select record.ID).First();
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, result);
                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { controller = "Books", id = Id }));

                    return response;
                }
                catch(Exception e)
                {
                    logger.Error("Exception Occurred during Adding the book");
                    logger.Error(e);
                    return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed,e);
                }

                //}

                //return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                logger.Error("Invalid ModelState");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ModelState);
            }
        }

        public HttpResponseMessage PutBook(Books books)
        {
            if (!ModelState.IsValid)
            {
                logger.Error("Invalid ModelState");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }


            //if (books.ISBN != null && books.BookName != null && books.Price != null && books.Details != null && books.AuthorName != null && books.Surname != null)
            //{
                try
                {
                    ObjectParameter responseMessage = new ObjectParameter("responseMessage", typeof(string));
                    db.EditBook(AccountController.UserId, books.ID, books.ISBN, books.BookName, books.Price, books.Details, books.ImageUrl, books.DownloadUrl, books.AuthorName, books.Surname, responseMessage);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    logger.Error("Exception Occurred during Updating the book");
                    logger.Error(ex);
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
        //        return Request.CreateResponse(HttpStatusCode.NoContent);

        //}

        public HttpResponseMessage DeleteBook(Books books)
        {
            //var response = new HttpResponseMessage();
            var user = AccountController.UserId;
            var isAdmin = db.Users.Find(user).IsAdmin;
            Book book = db.Books.Find(books.ID);
            if (isAdmin)
            {
                
                try
                {
                    ObjectParameter responseMessage = new ObjectParameter("responseMessage", typeof(string));

                    db.DeleteBook(user, books.ID, responseMessage);
                    
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    logger.Error("Exception during deleting the book");
                    logger.Error(ex);
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
                }
                logger.Info("Book Deleted");
                return Request.CreateResponse(HttpStatusCode.OK, book);
            }
            logger.Info("Not An Admin");
            return Request.CreateResponse(HttpStatusCode.Unauthorized, book);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}