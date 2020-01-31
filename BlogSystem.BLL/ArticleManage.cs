using BlogSystem.DAL;
using BlogSystem.Dto;
using BlogSystem.IBLL;
using BlogSystem.IDAL;
using BlogSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.BLL
{
    public class ArticleManage : IArticleManage
    {
        public async Task BedCountAdd(Guid articleId)
        {
            using (IArticleService articleService = new ArticleService())
            {
                var article = await articleService.GetOneByIdAsync(articleId);
                article.BadCount++;
                await articleService.EditAsync(article);
            }
        }

        public async Task CreateArticle(string title, string content, Guid[] categoryIds, Guid userId)
        {
            using(var articleService=new ArticleService())
            {
                var article=new Article()
                {
                    Title = title,
                    Content = content,
                    UserId = userId
                };
                await articleService.CreateAsync(article);
                Guid articleId = article.Id;
                using (var articleToCategoryService = new ArticleToCategoryService())
                {
                    foreach (var caregoryId in categoryIds)
                    {
                        await articleToCategoryService.CreateAsync(new ArticleToCategory()
                        {
                            ArticleId = articleId,
                            BlogCategoryId = caregoryId,
                        }, saved: false);
                    }
                    await articleToCategoryService.Save();
                   
                }
            }
        }

        public async Task CreateCategory(string name, Guid userid)
        {
            using (var CategoryService = new BlogCagetoryService())
            {
                await CategoryService.CreateAsync(new BlogCagetory()
                {
                    UserId=userid,
                    Cagetory=name
                });
            }
        }

        public async Task CreateComment(Guid userId, Guid ArticleId, string content)
        {
            using(ICommentService commentService=new CommentService())
            {
                await commentService.CreateAsync(new Comment()
                {
                    UserId = userId,
                    ArticleId = ArticleId,
                    Content = content
                });
            }
        }

        public async Task EditArticle(Guid articleId, string title, string content, Guid[] categoryIds)
        {
            using(IArticleService articleService=new ArticleService())
            {
               var article = await articleService.GetOneByIdAsync(articleId);
                article.Title = title;
                article.Content = content;
                await articleService.EditAsync(article);
            }
            using (IArticleToCategoryService articleToCategoryService=new ArticleToCategoryService())
                {
                    //删除原有类别
                    foreach (var categoryId in articleToCategoryService.GetAll().Where(m=>m.ArticleId==articleId))
                    {
                        await articleToCategoryService.RemoveAsync(categoryId,saved:false);
                    }
                if (categoryIds != null)
                {
                    foreach (var categoryId in categoryIds)
                    {
                        await articleToCategoryService.CreateAsync(new ArticleToCategory()
                        {
                            ArticleId = articleId,
                            BlogCategoryId = categoryId
                        }, saved: false);
                    }
                }
                    await articleToCategoryService.Save();
                }
            
        }

        public async Task EditCategory(Guid categoryId, string newCategoryName)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 判断文章是否存在
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public async Task<bool> ExistsArticle(Guid articleId)
        {
            using (IArticleService articleService=new ArticleService())
            {
              return await  articleService.GetAll().AnyAsync(m => m.Id == articleId);

            }
        }

        public async Task<List<ArticleDto>> GetAllArticleByCategoryId(Guid CategoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ArticleDto>> GetAllArticleByEamil(string eamil)
        {
            using (var articleService = new ArticleService())
            {
                var list = await articleService.GetAllByPageOrder().Where(m => m.User.Email == eamil).Select(m => new ArticleDto()
                {
                    Title = m.Title,
                    BadCount = m.BadCount,
                    GoodCount = m.GoodCount,
                    Email = m.User.Email,
                    Content = m.Content,
                    CreateTime = m.CreateTime,
                    Id = m.Id,
                    ImagePath = m.User.ImagePath
                }).ToListAsync();
                using (IArticleToCategoryService articleToCategoryService = new ArticleToCategoryService())
                {
                    foreach (var articleDto in list)
                    {
                        var cates = await articleToCategoryService.GetAll().Include(m => m.BlogCagetory).Where(m => m.ArticleId == articleDto.Id).ToListAsync();
                        articleDto.CategoryIds = cates.Select(m => m.BlogCategoryId).ToArray();
                        articleDto.CategoryNames = cates.Select(m => m.BlogCagetory.Cagetory).ToArray();
                    }
                    return list;
                }

            }
        }

        public async Task<List<ArticleDto>> GetAllArticleByUserId(Guid userId, int pageIndex, int pageSize)
        {
            using (var articleService = new ArticleService())
            {
                var list= await articleService.GetAllByPageOrder(pageSize,pageIndex,false).Where(m => m.UserId == userId).Select(m=>new ArticleDto()
                {
                    Title=m.Title,
                    BadCount=m.BadCount,
                    GoodCount=m.GoodCount,
                    Email=m.User.Email,
                    Content=m.Content,
                    CreateTime=m.CreateTime,
                    Id=m.Id,
                    ImagePath=m.User.ImagePath
                }).ToListAsync();
                using (IArticleToCategoryService articleToCategoryService =new ArticleToCategoryService())
                {
                    foreach (var articleDto in list)
                    {
                        var cates= await articleToCategoryService.GetAll().Include(m=>m.BlogCagetory).Where(m => m.ArticleId == articleDto.Id).ToListAsync();
                        articleDto.CategoryIds = cates.Select(m => m.BlogCategoryId).ToArray();
                        articleDto.CategoryNames = cates.Select(m => m.BlogCagetory.Cagetory).ToArray();
                    }
                    return list;
                }

            }
        }

        public async Task<List<BlogCagetoryDto>> GetAllCagetory(Guid userId)
        {
            using (IBlogCagetory cagetoryService = new BlogCagetoryService())
            {
                return await cagetoryService.GetAll().Select(m => new BlogCagetoryDto()
                {
                    Id = m.Id,
                    CagetoryName = m.Cagetory

                }).ToListAsync();
            }
        }

        public async Task<int> GetDataCount(Guid userId)
        {
           using(IArticleService articleService=new ArticleService())
            {
                return await articleService.GetAll().CountAsync(m => m.UserId == userId);
            }
        }

        public async Task<ArticleDto> GetOneArticleById(Guid articleId)
        {
            using (IArticleService articleService = new ArticleService())
            {
                var data= await articleService.GetAll().Include(m => m.User).
                    Where(m => m.Id == articleId).
                    Select(m=>new ArticleDto() 
                    { 
                        Title=m.Title,
                        Id=m.Id,
                        BadCount=m.BadCount,
                        Content=m.Content,
                        CreateTime=m.CreateTime,
                        Email=m.User.Email,
                        GoodCount=m.GoodCount,
                        ImagePath=m.User.ImagePath
                    }).FirstAsync();

                using (IArticleToCategoryService articleToCategoryService = new ArticleToCategoryService())
                {
                        var cates = await articleToCategoryService.GetAll().
                            Include(m => m.BlogCagetory).
                            Where(m => m.ArticleId == data.Id).ToListAsync();
                        data.CategoryIds = cates.Select(m => m.BlogCategoryId).ToArray();
                        data.CategoryNames = cates.Select(m => m.BlogCagetory.Cagetory).ToArray();
                    return data;
                }
            }
        }

        public async Task GoodCountAdd(Guid articleId)
        {
            using(IArticleService articleService=new ArticleService())
            {
                var article = await articleService.GetOneByIdAsync(articleId);
                article.GoodCount++;
                await articleService.EditAsync(article);
            }
        }

        public async Task RemoveArticle(Guid articleId)
        {
            using (IArticleService articleService = new ArticleService())
            {
                await articleService.RemoveAsync(articleId);
            }
        }

        public async Task RemoveCategory(string name)
        {

        }
        public async Task<List<CommentDto>> GetCommentsByArticleId(Guid articleId)
        {
            using(ICommentService commentService=new CommentService())
            {
               return await commentService.GetAllByOrder(false).Where(m=>m.ArticleId==articleId).Select(m=>new CommentDto() { 
                    Id=m.Id,
                    ArticleId=m.ArticleId,
                    UserId=m.UserId,
                    Eamil=m.User.Email,
                    Content=m.Content,
                    CreatTime=m.CreateTime
                }).ToListAsync();
            }
        }
    }
}
