using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using MyBlog2.Models;

namespace MyBlog2.Services
{
    public class UserService
    {
        private readonly MyBlogEntities _db = new MyBlogEntities();

        public User Login(User user)
        {
            user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(user.Password,
                "MD5");
            return _db.User.FirstOrDefault(t => t.Account == user.Account && t.Password == user.Password);
        }
    }
}