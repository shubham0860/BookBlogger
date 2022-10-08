﻿using BookBlogger.Data;
using BookBlogger.Web.Models;
using NLog;
using System;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace BookBlogger.Web.Controllers
{

    public class AccountController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private BooksBloggerEntities  entities = new BooksBloggerEntities();
        private object varhashedBytes;
        public static int UserId;
        public AccountController()
        {
            this.entities = new BooksBloggerEntities();
        }

        [Route("api/account/register")]
        
        [HttpPost]
        
        public void Register(Users model)
        {
            //model.IsAdmin = true;

            if (ModelState.IsValid)
            {
                using (entities)
                {
                    try
                    {
                        model.IsAdmin = false;
                        ObjectParameter responseMessage = new ObjectParameter("responseMessage", typeof(string));
                        
                        entities.AddUser(model.Username, model.Password, model.FirstName, model.LastName, model.IsAdmin, responseMessage);
                        entities.SaveChanges();
                    }
                    catch(Exception e)
                    {
                        logger.Error("Error Occoured During Adding User Data In Database");
                        logger.Error(e);

                    }

                }
            }
            logger.Error("ModelState is Invalid");

        }

        [Route("api/account/login")]
        [HttpPost]
        
        public HttpResponseMessage Login(Users userLogin)
        {
            if (ModelState.IsValid)
            {
                using (entities)
                {
              
                    using (var sha512 = SHA512.Create())
                    {
                        // Send a sample text to hash.
                        try
                        {
                            varhashedBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(userLogin.Password));
                        }
                        catch(Exception e)
                        {
                            logger.Error("Error During hashing the Password During login");
                            logger.Error(e);

                        }

                        // Get the hashed string.  
                        //var PassHash = BitConverter.ToString((byte[])varhashedBytes).Replace("-", "").ToLower();
                        try
                        {
                            var _currentUser = entities.Users.FirstOrDefault(user => user.Username == userLogin.Username && user.PasswordHash == varhashedBytes);
                            if (_currentUser != null)
                            {
                                UserId = _currentUser.ID;
                                //TODO: Redirect To Single app Page with this UserId 
                                var newUrl = this.Url.Link("Default", new
                                {
                                    Controller = "Book",
                                    Action = "Index"
                                });
                                return Request.CreateResponse(HttpStatusCode.Created,
                                                         new { Success = true, RedirectUrl = newUrl });
                            }
                        }
                        catch(Exception e)
                        {
                            logger.Error("Error Occoured During User Validation");
                            logger.Error(e);

                        }


                    }

                }
            }
            logger.Error("Invalid User");
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
