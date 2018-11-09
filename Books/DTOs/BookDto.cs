using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Books.DTOs
{
    public class BookDto
    {
        /// <summary>
        /// 书名
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public string Genre { get; set; }
    }
}