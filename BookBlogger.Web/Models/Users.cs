using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookBlogger.Web.Models
{
    public class Users
    {
        public int ID { get; set; }
        [Required]
        [StringLength(maximumLength: 40, MinimumLength = 5,
        ErrorMessage = "Username {0} should have {1} maximum characters and {2} minimum characters")]
        public string Username { get; set; }
        [Required]
        [StringLength(maximumLength: 40, MinimumLength = 5,
         ErrorMessage = "Password {0} should have {1} maximum characters and {2} minimum characters")]
        public string Password { get; set; }
        [Required]
        [StringLength(maximumLength: 40, MinimumLength = 3,
        ErrorMessage = "FirstName {0} should have {1} maximum characters and {2} minimum characters")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(maximumLength: 40, MinimumLength = 5,
        ErrorMessage = "Lastname {0} should have {1} maximum characters and {2} minimum characters")]
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
    }
}