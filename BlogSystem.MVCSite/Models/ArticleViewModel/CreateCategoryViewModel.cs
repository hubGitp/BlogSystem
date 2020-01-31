using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogSystem.MVCSite.Models.ArticleViewModel
{
    public class CreateCategoryViewModel
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        /// 
        [Required]
        [StringLength(maximumLength:200,MinimumLength =2)]
        [Display(Name ="类型名称")]
        public string CategoryName { get; set; }
    }
}