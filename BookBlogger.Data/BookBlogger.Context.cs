﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class BooksBloggerEntities : DbContext
    {
        public BooksBloggerEntities()
            : base("name=BooksBloggerEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Audit> Audits { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Books_Authors> Books_Authors { get; set; }
        public virtual DbSet<Users_Books> Users_Books { get; set; }
        public virtual DbSet<BookView> BookViews { get; set; }
    
        public virtual int AddUser(string username, string password, string firstName, string lastName, Nullable<bool> isAdmin, ObjectParameter responseMessage)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("LastName", lastName) :
                new ObjectParameter("LastName", typeof(string));
    
            var isAdminParameter = isAdmin.HasValue ?
                new ObjectParameter("IsAdmin", isAdmin) :
                new ObjectParameter("IsAdmin", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddUser", usernameParameter, passwordParameter, firstNameParameter, lastNameParameter, isAdminParameter, responseMessage);
        }
    
        public virtual int AddAuthor(Nullable<int> bookID, string firstName, string lastName, Nullable<System.DateTime> createdDateTime, Nullable<System.DateTime> updatedDateTime)
        {
            var bookIDParameter = bookID.HasValue ?
                new ObjectParameter("BookID", bookID) :
                new ObjectParameter("BookID", typeof(int));
    
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("LastName", lastName) :
                new ObjectParameter("LastName", typeof(string));
    
            var createdDateTimeParameter = createdDateTime.HasValue ?
                new ObjectParameter("CreatedDateTime", createdDateTime) :
                new ObjectParameter("CreatedDateTime", typeof(System.DateTime));
    
            var updatedDateTimeParameter = updatedDateTime.HasValue ?
                new ObjectParameter("UpdatedDateTime", updatedDateTime) :
                new ObjectParameter("UpdatedDateTime", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddAuthor", bookIDParameter, firstNameParameter, lastNameParameter, createdDateTimeParameter, updatedDateTimeParameter);
        }
    
        public virtual int AddBook(Nullable<int> userID, string iSBN, string bookName, Nullable<decimal> price, string details, string imageUrl, string downloadUrl, string firstName, string lastName, ObjectParameter responseMessage)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var iSBNParameter = iSBN != null ?
                new ObjectParameter("ISBN", iSBN) :
                new ObjectParameter("ISBN", typeof(string));
    
            var bookNameParameter = bookName != null ?
                new ObjectParameter("BookName", bookName) :
                new ObjectParameter("BookName", typeof(string));
    
            var priceParameter = price.HasValue ?
                new ObjectParameter("Price", price) :
                new ObjectParameter("Price", typeof(decimal));
    
            var detailsParameter = details != null ?
                new ObjectParameter("Details", details) :
                new ObjectParameter("Details", typeof(string));
    
            var imageUrlParameter = imageUrl != null ?
                new ObjectParameter("ImageUrl", imageUrl) :
                new ObjectParameter("ImageUrl", typeof(string));
    
            var downloadUrlParameter = downloadUrl != null ?
                new ObjectParameter("DownloadUrl", downloadUrl) :
                new ObjectParameter("DownloadUrl", typeof(string));
    
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("LastName", lastName) :
                new ObjectParameter("LastName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddBook", userIDParameter, iSBNParameter, bookNameParameter, priceParameter, detailsParameter, imageUrlParameter, downloadUrlParameter, firstNameParameter, lastNameParameter, responseMessage);
        }
    
        public virtual ObjectResult<ReadBooks_Result> ReadBooks(ObjectParameter responseMessage)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ReadBooks_Result>("ReadBooks", responseMessage);
        }
    
        public virtual int EditBook(Nullable<int> userID, Nullable<int> bookID, string iSBN, string bookName, Nullable<decimal> price, string details, string imageUrl, string downloadUrl, string authorName, string surname, ObjectParameter responseMessage)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var bookIDParameter = bookID.HasValue ?
                new ObjectParameter("BookID", bookID) :
                new ObjectParameter("BookID", typeof(int));
    
            var iSBNParameter = iSBN != null ?
                new ObjectParameter("ISBN", iSBN) :
                new ObjectParameter("ISBN", typeof(string));
    
            var bookNameParameter = bookName != null ?
                new ObjectParameter("BookName", bookName) :
                new ObjectParameter("BookName", typeof(string));
    
            var priceParameter = price.HasValue ?
                new ObjectParameter("Price", price) :
                new ObjectParameter("Price", typeof(decimal));
    
            var detailsParameter = details != null ?
                new ObjectParameter("Details", details) :
                new ObjectParameter("Details", typeof(string));
    
            var imageUrlParameter = imageUrl != null ?
                new ObjectParameter("ImageUrl", imageUrl) :
                new ObjectParameter("ImageUrl", typeof(string));
    
            var downloadUrlParameter = downloadUrl != null ?
                new ObjectParameter("DownloadUrl", downloadUrl) :
                new ObjectParameter("DownloadUrl", typeof(string));
    
            var authorNameParameter = authorName != null ?
                new ObjectParameter("AuthorName", authorName) :
                new ObjectParameter("AuthorName", typeof(string));
    
            var surnameParameter = surname != null ?
                new ObjectParameter("Surname", surname) :
                new ObjectParameter("Surname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EditBook", userIDParameter, bookIDParameter, iSBNParameter, bookNameParameter, priceParameter, detailsParameter, imageUrlParameter, downloadUrlParameter, authorNameParameter, surnameParameter, responseMessage);
        }
    
        public virtual int DeleteBook(Nullable<int> userID, Nullable<int> bookID, ObjectParameter responseMessage)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var bookIDParameter = bookID.HasValue ?
                new ObjectParameter("BookID", bookID) :
                new ObjectParameter("BookID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteBook", userIDParameter, bookIDParameter, responseMessage);
        }
    
        public virtual ObjectResult<ReadBook_Result> ReadBook(Nullable<int> userID, ObjectParameter responseMessage)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ReadBook_Result>("ReadBook", userIDParameter, responseMessage);
        }
    }
}
