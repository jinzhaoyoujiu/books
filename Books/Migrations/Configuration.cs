namespace Books.Migrations
{
    using Books.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Books.Models.BooksContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Books.Models.BooksContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            //context.Authors.AddOrUpdate(
            //    new Author{ Books=
            //    new List<Book> {
            //        new Book { Genre="a", Title="钢铁是怎么练车的", PublicationDate=Convert.ToDateTime("2018/12/12") },
            //        new Book{ Genre="b",Title="楚天雷和",PublicationDate=Convert.ToDateTime("2018/12/1")},
            //        new Book{ Genre="c",Title="尘埃落定",PublicationDate=Convert.ToDateTime("2018/12/3")}
            //    }, Name="dufu"
            //    });
        }
    }
}
