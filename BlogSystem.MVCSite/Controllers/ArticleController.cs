using BlogSystem.BLL;
using BlogSystem.IBLL;
using BlogSystem.MVCSite.Filters;
using BlogSystem.MVCSite.Models.ArticleViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlogSystem.MVCSite.Controllers
{
    [BlogStstemAuth]
    public class ArticleController : Controller
    {
        
        // GET: Article
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult CreateCategory(CreateCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                IArticleManage articleManage = new ArticleManage();
                articleManage.CreateCategory(model.CategoryName, userid: Guid.Parse(Session["userId"].ToString()));
                return RedirectToAction("CategoryList");

            }
            ModelState.AddModelError(key: "", errorMessage: "类型创建失败");
            return View(model);
        }
        [HttpGet]

        public async Task <ActionResult> CategoryList()
        {
            var userid = Guid.Parse(Session["userId"].ToString());
            return View( model:await new ArticleManage().GetAllCagetory(userid));
        }

        [HttpGet]

        public async Task<ActionResult> CreateArticle()
        {
            var userid = Guid.Parse(Session["userId"].ToString());
            ViewBag.CategoryIds = await new ArticleManage().GetAllCagetory(userid);
            return View();
        }
        [HttpPost]

        public async Task<ActionResult> CreateArticle(CreateArtcleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userid = Guid.Parse(Session["userId"].ToString());
                await new ArticleManage().CreateArticle(model.Title, model.Content, model.CategoryIds, userid);
                return RedirectToAction("ArticleList");
            }
            else
            {
                ModelState.AddModelError(key: "", errorMessage: "创建文章失败");
            }
            return View(model);
        }
        [HttpGet]

        public async Task<ActionResult> ArticleList(int PageIndex = 0, int PageSize = 7)
        {
            //告诉前端 总页码数，当前页码，可显示的页码数

            var userid = Guid.Parse(Session["userId"].ToString());
            var artcles= await new ArticleManage().GetAllArticleByUserId(userid, PageIndex, PageSize);
            var dateCount = await new ArticleManage().GetDataCount(userid);
            ViewBag.PageCount = dateCount % PageSize == 0 ? dateCount/PageSize:dateCount/PageSize+1;
            ViewBag.PageIndex = PageIndex;
            return View(artcles);
        }

        public async Task<ActionResult> ArticleDetails(Guid? id)
        {
            var ArticleManage = new ArticleManage();
            if (id == null || !await new ArticleManage().ExistsArticle(id.Value))
                return RedirectToAction(nameof(ArticleList));
            ViewBag.Comments =await  new ArticleManage().GetCommentsByArticleId(id.Value);
            return View(await ArticleManage.GetOneArticleById(id.Value));
        }
        [HttpGet]
        public async Task<ActionResult> EditArticle(Guid id)
        {
            var articleManage = new ArticleManage();
            var data = await articleManage.GetOneArticleById(id);
            var userid = Guid.Parse(Session["userId"].ToString());
            ViewBag.CategoryIds = await new ArticleManage().GetAllCagetory(userid);
            return View(new EditArticleViewModel(){ 
                Id=data.Id,
                Title=data.Title,
                Content=data.Content,
                CategoryIds=data.CategoryIds
            });
        }

        [HttpPost]
        public async Task<ActionResult> EditArticle(EditArticleViewModel model)
        {
            if(ModelState.IsValid)
            {
                var articleManage = new ArticleManage();
                await  articleManage.EditArticle(model.Id, model.Title, model.Content, model.CategoryIds);
                return RedirectToAction("ArticleList");
            }
            else
            {
                var userid = Guid.Parse(Session["userId"].ToString());
                ViewBag.CategoryIds = await new ArticleManage().GetAllCagetory(userid);
                return View(model);
            }
        }

        [HttpPost]
        public async Task<ActionResult> GoodCount(Guid id)
        {
            IArticleManage articleManage = new ArticleManage();
            await articleManage.GoodCountAdd(id);
            return Json(data:new { result="ok"});
        }
        [HttpPost]
        public async Task<ActionResult> BedCount(Guid id)
        {
            IArticleManage articleManage = new ArticleManage();
            await articleManage.BedCountAdd(id);
            return Json(data: new { result = "ok" });
        }
        [HttpPost]
        public async Task<ActionResult> AddComment(CreateCommentViewModel model)
        {
            var userid = Guid.Parse(Session["userId"].ToString());
            IArticleManage articleManage = new ArticleManage();
            await  articleManage.CreateComment(userid, model.Id, model.Content);
            return Json(data: new { result = "ok" });
        }
    }
}