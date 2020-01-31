using BlogSystem.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.IBLL
{
    public  interface IArticleManage
    {
        /// <summary>
        /// 创建文章
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="categoryIds"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task CreateArticle(string title,string content,Guid[] categoryIds,Guid userId);

        Task CreateCategory(string name, Guid userid);
        Task<List<BlogCagetoryDto>> GetAllCagetory(Guid userId);

        Task<List<ArticleDto>> GetAllArticleByUserId(Guid userId,int pageIndex,int pageSize);
        Task<List<ArticleDto>> GetAllArticleByEamil(string eamil);
        Task<List<ArticleDto>> GetAllArticleByCategoryId(Guid CategoryId);
        Task RemoveCategory(string name);
        Task EditCategory(Guid categoryId, string newCategoryName);
        Task RemoveArticle(Guid articleId);
        Task EditArticle(Guid articleId, string title, string content, Guid[] categoryIds);

        Task<bool> ExistsArticle(Guid articleId);
        Task<ArticleDto> GetOneArticleById(Guid articleId);
        Task<int> GetDataCount(Guid userId);

        Task GoodCountAdd(Guid articleId);
        Task BedCountAdd(Guid articleId);

        Task CreateComment(Guid userId, Guid ArticleId, string content);

        Task<List<CommentDto>> GetCommentsByArticleId(Guid articleId);

    }
}
