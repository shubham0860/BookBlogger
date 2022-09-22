using BookBlogger.Data;
using BookBlogger.Models;
using BookBlogger.Web.Models;
using System;
using System.Collections.Generic;
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
        private BookBloggerEntities  bookBloggerEntities = new BookBloggerEntities();
        private object varhashedBytes;

        //[System.Web.Http.Route("register")]
        [HttpPost]
        public void Register(Users model)
        {
            //model.IsAdmin = true;

            if (ModelState.IsValid)
            {
                using (BookBloggerEntities entities = new BookBloggerEntities())
                {
                    //var user = 
                    model.IsAdmin = false;
                    model.CreatedDateTime = DateTime.Today;
                    entities.uspAddUser(model.Username, model.Password, model.FirstName, model.LastName, model.IsAdmin, model.CreatedDateTime);
                    //entities.Users.Add(model);
                    entities.SaveChanges();   
                }
            }
            // return View(model);
        }

        //[System.Web.Http.Route("login")]
        [HttpPost]
        public void Login(UserLogin userLogin)
        {
            if (ModelState.IsValid)
            {
                using (BookBloggerEntities entities = new BookBloggerEntities())
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
                        }
                        else
                        {
                            Console.WriteLine("Invalid");
                        }
                    }

                }
            }
        }
    }
}
