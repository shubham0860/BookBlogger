using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookBlogger.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBlogger.Data;
using Kendo.Mvc.UI;
using BookBlogger.Web.Models;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http.Hosting;

namespace BookBlogger.Web.Controllers.Tests
{
    [TestClass()]
    public class BooksControllerTests
    {
       
        [TestMethod()]
        public void GetBooksTest()
        {
            //Arrange
            var controller = new BooksController();
            DataSourceRequest request = new DataSourceRequest();

            //Act
            var Get_Books_result = controller.GetBooks(request);


            //Assert
            Assert.IsNotNull(Get_Books_result.Data);
        }


        [TestMethod()]
        public void UserBooksTest()
        {
            //Arrange
            var controller = new BooksController();
            DataSourceRequest request = new DataSourceRequest();
            AccountController.UserId = 8;

            //Act
            var Get_UserBooks_result = controller.UserBooks(request);

            //Assert
            Assert.IsNotNull(Get_UserBooks_result.Data);
            
        }

        [TestMethod()]
        public void GetAuditsTest()
        {
            //Arrange
            var controller = new BooksController();
            DataSourceRequest request = new DataSourceRequest();
            

            //Act
            var Get_Audit_result = controller.GetAudits(request);

            //Assert
            Assert.IsNotNull(Get_Audit_result.Data);
        }

        [TestMethod()]
        public void PostBookTest()
        {
            //Arrange
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44367/api/books");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            BooksController controller = new BooksController
            {
                Request = request,
            };
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            AccountController.UserId = 2;
            Books book = new Books();

            //book.ID = 1;
            book.BookName = "Life In a Metro";
            book.AuthorName = "Smith";
            book.Details = "Fiction";
            book.ISBN = "JHOPW96QNP";
            book.Price = 890;
            book.Surname = "Taylor";


            //Act
            var Add_Book_result = controller.PostBook(book);

            //Assert
            Assert.IsTrue(Add_Book_result.IsSuccessStatusCode);
        }

        [TestMethod()]
        public void PutBookTest()
        {
            //Arrange
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Put, "https://localhost:44367/api/books");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            BooksController controller = new BooksController
            {
                Request = request,
            };
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            AccountController.UserId = 2;
            Books book = new Books();
            book.ID = 1;
            book.BookName = "Jhon is My Name";
            book.AuthorName = "Jhon";
            book.Details = "Autobiography";
            book.ISBN = "JH09876QNP";
            book.Price = 400;
            book.Surname = "Roman";

            //Act
            var Update_result = controller.PutBook(book);

            //Assert
            Assert.IsTrue(Update_result.IsSuccessStatusCode);
        }

        [TestMethod()]
        public void DeleteBookTest()
        {
            //Arrange
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Delete, "https://localhost:44367/api/books");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            BooksController controller = new BooksController
            {
                Request = request,
            };
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            AccountController.UserId = 8;
            Books book = new Books();
            book.ID = 20;

            //Act
            var Delete_Book_result = controller.DeleteBook(book);

            //Assert
            Assert.IsTrue(Delete_Book_result.IsSuccessStatusCode);
        }
    }
}