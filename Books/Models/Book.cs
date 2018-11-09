using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Books.Models
{
    public class Book
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        public string Title { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public int AuthorID { get; set; }
        [ForeignKey("AuthorID")]
        [JsonIgnore]
        public virtual Author Author { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        [JsonIgnore]
        public string Genre { get; set; }
        /// <summary>
        /// 发行时间
        /// </summary>
        [JsonIgnore]
        public DateTime PublicationDate { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        [JsonIgnore]
        public decimal Price { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [JsonIgnore]
        public string Description { get; set; }

    }
}