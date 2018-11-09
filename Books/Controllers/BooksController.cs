using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Books.Models;
using System.Data.Entity;
using Books.DTOs;
using System.Web.Http.Description;

namespace Books.Controllers
{
    [RoutePrefix("api/Books")]
    public class BooksController : ApiController
    {
        private BooksContext db = new BooksContext();
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private static readonly System.Linq.Expressions.Expression<Func<Book, BookDto>> AsBookDto = x => new BookDto
        {
            Author = x.Author.Name,
            Genre = x.Genre,
            Title = x.Title
        };
        private static readonly System.Linq.Expressions.Expression<Func<Book, BookDetailDto>> AsBookDetailDto = x => new BookDetailDto
        {
            Author = x.Author.Name,
            Description = x.Description,
            Genre = x.Genre,
            Price = x.Price,
            PublishDate = x.PublicationDate,
            Title = x.Title
        };
        //private delegate BookDto deleBookDto(Book book);
        //private BookDto GetBookDto(Book book)
        //{
        //    return new BookDto
        //    {
        //        Author = book.Author.Name,
        //        Genre = book.Genre,
        //        Title = book.Title
        //    };
        //}
        /// <summary>
        /// 获取全部图书
        /// </summary>
        /// <returns></returns>
        // GET api/<controller>
        [Route("")]
        public IEnumerable<BookDto> Get()
        {
            //deleBookDto AsBookDto = GetBookDto;
            IEnumerable<BookDto> books = db.Books.Include(x => x.Author).Select(AsBookDto);
            return books;
        }
        /// <summary>
        /// 通过图书ID获取图书信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:int}")]
        [ResponseType(typeof(BookDetailDto))]
        // GET api/<controller>/5
        public async Task<IHttpActionResult> Get(int id)
        {
            await RunAsync();
            var bookDetail =await db.Books.Include(x => x.Author).Where(x => x.Id == id).Select(AsBookDetailDto).FirstOrDefaultAsync();
            if (bookDetail == null)
            {
                return NotFound();
            }
            else {
                return Ok(bookDetail);
            }
        }
        /// <summary>
        /// 通过图书类别获取图书信息
        /// </summary>
        /// <param name="genre"></param>
        /// <returns></returns>
        [Route("{genre:alpha:length(1,3)}")]
        public IEnumerable<BookDto> GetBooksByGenre(string genre)
        {
            return  db.Books.Where(x=>x.Genre.Equals(genre,StringComparison.OrdinalIgnoreCase)).Include(x=>x.Author).Select(AsBookDto);
        }
        /// <summary>
        /// 通古作者id获取图书
        /// </summary>
        /// <param name="authorId"></param>
        /// <returns></returns>
        [Route("~/api/author/{authorId:int}/books")]
        public IEnumerable<BookDto> GetBooksByAuthor(int authorId)
        {
            return db.Books.Where(x=>x.AuthorID==authorId).Include(x => x.Author).Select(AsBookDto);
        }
        /// <summary>
        /// 通过发行时间获取图书
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [Route("{date:datetime:regex(\\d{4}-\\d{2}-\\d{2})}")]
        [Route("{*date:datetime:regex(\\d{4}/\\d{2}/\\d{2})}")]
        public IEnumerable<BookDto> GetBooksByDate(DateTime date)
        {
            return db.Books.Where(x =>DbFunctions.TruncateTime(x.PublicationDate)==DbFunctions.TruncateTime(date)).Include(x => x.Author).Select(AsBookDto);
        }
        public async Task<IHttpActionResult> Post(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else {
                db.Books.Add(book);
                await db.SaveChangesAsync();
                return Ok();
            }
        }
        static async Task RunAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59241");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/bson"));

                #region get
                var result = await client.GetAsync("api/books/1");
                try
                {
                    result.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                }

                System.Net.Http.Formatting.MediaTypeFormatter[] formatters = new System.Net.Http.Formatting.MediaTypeFormatter[] {
                    new System.Net.Http.Formatting.BsonMediaTypeFormatter()
                };
                var book = await result.Content.ReadAsAsync<Book>(formatters);
                #endregion
                var book2 = new Book {
                     AuthorID=1,
                     Title="tutu",
                };

                System.Net.Http.Formatting.BsonMediaTypeFormatter typeFormatter = new System.Net.Http.Formatting.BsonMediaTypeFormatter();
                var result2 = await client.PostAsync("api/books", book2, typeFormatter);
                result2.EnsureSuccessStatusCode();
            }
        }
        
    }
}