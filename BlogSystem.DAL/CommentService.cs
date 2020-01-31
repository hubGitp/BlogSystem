using BlogSystem.IDAL;
using BlogSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DAL
{
    public  class CommentService:BaseService<Comment>,ICommentService
    {
        public CommentService() : base(new  BlogContext())
        {

        }
    }
}
