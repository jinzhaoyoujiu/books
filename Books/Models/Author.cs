using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Books.Models
{
    public class Author
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        [Required]
        public string Name { get; set; }
        ///// <summary>
        ///// 书
        ///// </summary>
        //public virtual ICollection<Book> Books{get;set;}
    }
}