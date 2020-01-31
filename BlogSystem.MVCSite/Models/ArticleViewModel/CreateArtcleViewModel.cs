using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogSystem.MVCSite.Models.ArticleViewModel
{
    public class CreateArtcleViewModel
    {
        [Required]
        [Display(Name = "标题")]
        public string Title { get; set; }
        [Required]
        [Display(Name ="内容")]
        public string Content { get; set; }
        [Display(Name ="用户文章分类")]
        public Guid[] CategoryIds { get; set; }
    }
}