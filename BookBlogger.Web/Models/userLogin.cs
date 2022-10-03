using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookBlogger.Web.Models
{
    public class UserLogin
    {
        [Required]
        [StringLength(maximumLength: 40, MinimumLength = 5,
         ErrorMessage = "Username {0} should have {1} maximum characters and {2} minimum characters")]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}