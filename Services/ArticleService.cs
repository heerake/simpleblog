using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using MyBlog2.Models;

namespace MyBlog2.Services
{
    public class ArticleService
    {
        private readonly MyBlogEntities _db = new MyBlogEntities();

        public List<Article> GetAll()
        {
            return _db.Article.ToList();
        }

        public Article GetById(int id)
        {
            return _db.Article.FirstOrDefault(t => t.ID == id);
        }

        public List<Article> GetByCategoryId(int categoryId)
        {
            return _db.Article.Where(t => t.CategoryId == categoryId).ToList();
        } 

        public void Insert(Article article)
        {
            article.CreateOn = DateTime.Now;
            _db.Article.Add(article);
            _db.SaveChanges();
        }

        public void Update(Article article)
        {
            _db.Article.AddOrUpdate(article);
            _db.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var article = _db.Article.FirstOrDefault(t => t.ID == id);
            _db.Article.Remove(article);
            _db.SaveChanges();
        }
    }
}