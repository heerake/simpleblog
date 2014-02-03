using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlog2.Models;
using MyBlog2.Services;

namespace MyBlog2.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ArticleService _articleService = new ArticleService();
        private readonly CategoryService _categoryService = new CategoryService();
        private readonly List<Category> _categories = new List<Category>();

        public ArticleController()
        {
            _categories = _categoryService.GetAll();
        }

        public ActionResult Index()
        {
            ViewBag.Articles = _articleService.GetAll().OrderByDescending(t=>t.CreateOn);
            ViewBag.Categories = _categories;
            return View();
        }

        public ActionResult Detail(int id)
        {
            ViewBag.Categories = _categories;
            ViewBag.Article = _articleService.GetById(id);
            return View();
        }

        public ActionResult Category(int categoryId)
        {
            ViewBag.Category = categoryId;
            ViewBag.Categories = _categories;
            ViewBag.Articles = _articleService.GetByCategoryId(categoryId).OrderByDescending(t => t.CreateOn);
            return View("Index");
        }
    }
}
