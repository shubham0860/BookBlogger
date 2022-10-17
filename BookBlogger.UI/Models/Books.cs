using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookBlogger.UI.Models
{
    public class Books
    {
        public int ID { get; set; }
        public string ISBN { get; set; }
        public string BookName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Details { get; set; }
        public string ImageUrl { get; set; }
        public string DownloadUrl { get; set; }
        public string AuthorName { get; set; }
        public string Surname { get; set; }
    }
}