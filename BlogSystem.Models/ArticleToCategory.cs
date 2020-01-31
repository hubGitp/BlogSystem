using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Models
{
    /// <summary>
    /// 文章与类别多对多关系
    /// </summary>
    public  class ArticleToCategory:BaseEntity
    {
        [ForeignKey(nameof(BlogCagetory))]
        public Guid BlogCategoryId { get; set; }
        public BlogCagetory BlogCagetory { get; set; }
        [ForeignKey(nameof(Article))]
        public Guid ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
