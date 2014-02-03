using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using MyBlog2.Models;

namespace MyBlog2.Services
{
    public class CategoryService 
    {
        private readonly MyBlogEntities _db = new MyBlogEntities();

        public List<Category> GetAll()
        {
            return _db.Category.OrderBy(t => t.Order).ToList();
        }

        public void Update(Category[] categories)
        {
            var oriCategories = _db.Category.ToList();
            for (int i = 0, count = oriCategories.Count(); i < count; i++)
            {
                if (categories.FirstOrDefault(t => t.ID == oriCategories[i].ID) == null)
                {
                    _db.Category.Remove(oriCategories[i]);
                }
            }
            _db.SaveChanges();

            foreach (Category category in categories)
            {
                _db.Category.AddOrUpdate(category);
            }
            _db.SaveChanges();
        }
    }
}