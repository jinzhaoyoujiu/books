using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Books.Models
{
    public class BooksContext : DbContext
    {
        public BooksContext():base("sqlServerConnection")
        {
            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
    }
}