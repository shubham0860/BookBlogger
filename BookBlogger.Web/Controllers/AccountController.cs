using BookBlogger.Data;
using BookBlogger.Models;
using BookBlogger.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;

namespace BookBlogger.Web.Controllers
{
    //[System.Web.Mvc.RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        //private User _currentUser;
        private BooksBloggerEntities  entities = new BooksBloggerEntities();
        private object varhashedBytes;
        public AccountController()
        {
            this.entities = new BooksBloggerEntities();
        }
        //[System.Web.Http.Route("register")]
        [HttpPost]
        public void Register(Users model)
        {
            //model.IsAdmin = true;

            if (ModelState.IsValid)
            {
                using (entities)
                {
                    //var user = 
                    model.IsAdmin = false;
                    //model.CreatedDateTime = DateTime.Today;
                    ObjectParameter responseMessage = new ObjectParameter("responseMessage", typeof(string));

                    entities.AddUser(model.Username, model.Password, model.FirstName, model.LastName, model.IsAdmin, responseMessage);
                    //entities.Users.Add(model);
                    entities.SaveChanges();   
                }
            }
            // return View(model);
        }
        //public IEnumerable<User> Get()
        //{
        //    using(BooksBloggerEntities entities = new BooksBloggerEntities())
        //    {
        //        return entities.Users.ToList();
        //    }
        //}

        //[System.Web.Http.Route("login")]
        [HttpPost]
        public HttpResponseMessage Login(UserLogin userLogin)
        {
            if (ModelState.IsValid)
            {
                using (entities)
                {
                    //var user = 
                    //var PassHash = userLogin.Password;

                    using (var sha512 = SHA512.Create())
                    {
                        // Send a sample text to hash.  
                        varhashedBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(userLogin.Password));
                        // Get the hashed string.  
                        var PassHash = BitConverter.ToString((byte[])varhashedBytes).Replace("-", "").ToLower();

                        var _currentUser = entities.Users.FirstOrDefault(user => user.Username == userLogin.Username && user.PasswordHash == varhashedBytes);
                        if (_currentUser != null)
                        {
                            var UserId = _currentUser.ID;
                            //TODO: Redirect To Single app Page with this UserId
                            Debug.WriteLine("Success &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                            //Redirect("https://localhost:44302/Home/Contact");
                            
                            var newUrl = this.Url.Link("Default", new
                            {
                                Controller = "Book",
                                Action = "Index"
                            });
                            return Request.CreateResponse(HttpStatusCode.Created,
                                                     new { Success = true, RedirectUrl = newUrl });
                        }

                    }

                }
            }

            var testUrl = this.Url.Link("Default", new
            {
                Controller = "Book",
                Action = "Error"
            });
            return Request.CreateResponse(HttpStatusCode.Forbidden,
                                     new { Success = false, RedirectUrl = testUrl });
        }
    }
}
