//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookBlogger.Data
{
    using System;
    
    public partial class ReadBooks_Result
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
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public Nullable<System.DateTime> UpdatedDateTime { get; set; }
    }
}
