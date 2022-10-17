using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookBlogger.UI.Models
{
    public class Users
    {
        public int ID { get; set; }
        
        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
    }
}