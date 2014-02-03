using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MyBlog2.Models;
using MyBlog2.Services;

namespace MyBlog2.Controllers
{
    public class AdminController : Controller
    {
        private readonly ArticleService _articleService = new ArticleService();
        private readonly CategoryService _categoryService = new CategoryService();
        private readonly UserService _userService = new UserService();
        public ActionResult Login()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(User user)
        {
            var loginUser = _userService.Login(user);
            if (loginUser != null)
            {
                //var ticket = new FormsAuthenticationTicket(1, user.NickName, DateTime.Now, DateTime.Now.AddMinutes(30),
                //false, "none", FormsAuthentication.FormsCookiePath);      
                //string encTicket = FormsAuthentication.Encrypt(ticket);
                //Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                FormsAuthentication.SetAuthCookie(loginUser.NickName, true);
            }
            return RedirectToAction("Index", "Admin");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Admin");
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Article()
        {
            ViewBag.Articles = _articleService.GetAll().OrderByDescending(t => t.ID);
            return View();
        }

        [Authorize]
        public ActionResult ArticleCreate()
        {
            ViewBag.Categories = _categoryService.GetAll();
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ArticleCreate(Article article)
        {
            _articleService.Insert(article);
            return RedirectToAction("Article", "Admin");
        }

        [Authorize]
        public ActionResult ArticleDelete(int id)
        {
            _articleService.DeleteById(id);
            var json = new
            {
                statuscode = "400"
            };
            return Json(json);
        }


        [Authorize]
        public ActionResult ArticleEdit(int id)
        {
            ViewBag.Article = _articleService.GetById(id);
            ViewBag.Categories = _categoryService.GetAll();
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ArticleEdit(Article article)
        {
            _articleService.Update(article);
            return RedirectToAction("Article", "Admin");
        }

        [Authorize]
        public ActionResult Category()
        {
            ViewBag.Categories = _categoryService.GetAll();
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CategoryCreate(Category[] categories)
        {
            _categoryService.Update(categories);
            return RedirectToAction("Category", "Admin");
        }
    }
}
