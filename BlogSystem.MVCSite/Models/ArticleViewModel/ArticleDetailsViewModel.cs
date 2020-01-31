using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogSystem.MVCSite.Models.ArticleViewModel
{
    public class ArticleDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public string Eamil { get; set; }
        public string ImagePath { get; set; }
        public string[] CategoryIds { get; set; }
        public string[] CategoryNames { get; set; }
        public int GoodCount { get; set; }
        public int BedCount { get; set; }
    }
}