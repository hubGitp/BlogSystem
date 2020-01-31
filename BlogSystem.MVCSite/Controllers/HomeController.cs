using BlogSystem.BLL;
using BlogSystem.IBLL;
using BlogSystem.MVCSite.Filters;
using BlogSystem.MVCSite.Models;
using BlogSystem.MVCSite.Models.LoginViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlogSystem.MVCSite.Controllers
{
    public class HomeController : Controller
    {
        [BlogStstemAuth]
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)return View(model);

            IUserManage userManage = new UserManage();
            await userManage.Register(model.Eamil, model.Password);
            return Content("注册成功");
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                IUserManage userManage = new UserManage();
                if (userManage.Login(model.Email, password: model.Password, out Guid userId))
                {
                    //判断使用cookie还是session
                    //跳转
                    if (model.RememberMe)
                    {
                        Response.Cookies.Add(new HttpCookie(name: "loginName")
                        {
                            Value = model.Email,
                            Expires = DateTime.Now.AddDays(7)
                        });
                        Response.Cookies.Add(new HttpCookie(name: "userId")
                        {
                            Value = userId.ToString(),
                            Expires = DateTime.Now.AddDays(7)
                        });
                    }
                    else
                    {
                        Session["loginName"] = model.Email;
                        Session["userId"] = userId;
                    }
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(key: "", errorMessage: "您的账号密码有误");
                }
            }
            return View(model);
            
        }
    }
}