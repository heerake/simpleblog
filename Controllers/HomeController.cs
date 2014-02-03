using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlog2.Models;
using MyBlog2.Services;

namespace MyBlog2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ArticleService _articleService = new ArticleService();
        private readonly CategoryService _categoryService = new CategoryService();
        private readonly List<Category> _categories = new List<Category>();

        public HomeController()
        {
            _categories = _categoryService.GetAll();
        }

        public ActionResult Index()
        {
            return RedirectToAction("Index","Article");
        }

        public ActionResult About()
        {
            ViewBag.Categories = _categories;
            return View();
        }
    }
}
