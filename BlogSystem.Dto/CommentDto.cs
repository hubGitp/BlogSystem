using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Dto
{
    public  class CommentDto
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string Eamil { get; set; }
        public Guid  ArticleId { get; set; }
        public string Content { get; set; }
        public DateTime CreatTime { get; set; }
    }
}
