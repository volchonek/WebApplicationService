using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTfullAPIService.Models;

namespace RESTfullAPIService.Controllers
{
    public class CRUDController : Controller
    {
        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="id">Set id</param>
        /// <param name="name">Set username</param>
        public void CreateUser(int id, string name)
        {
            using (UserContext db = new UserContext())
            {
                User user = new User { Id = id, Name = name };

                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            using (UserContext db = new UserContext())
            {
                users = db.Users.ToList();
            }

            return users;
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">Find by id</param>
        /// <returns></returns>
        public User GetUserById(int id)
        {
            List<User> users = new List<User>();

            using (UserContext db = new UserContext())
            {
               users = db.Users.ToList();
            }

            return users.Find(u => u.Id == id);
        }

        /// <summary>
        /// Get user by name, return first find user.
        /// </summary>
        /// <param name="name">Find by name</param>
        /// <returns></returns>
        public User GetUserByName(string name)
        {
            List<User> users = new List<User>();

            using (UserContext db = new UserContext())
            {
                users = db.Users.ToList();
            }


            return users.Find(u => u.Name == name);
        }

        /// <summary>
        /// Edit username by id user
        /// </summary>
        /// <param name="id">Find by id</param>
        /// <param name="name"> For edit </param>
        public void EditUser(int id, string name)
        {
            List<User> users = new List<User>();

            using (UserContext db = new UserContext())
            {
                users = db.Users.ToList();
                var findUser= users.Find(u => u.Id == id);
                findUser.Name = name;

                db.Users.Update(findUser);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="id">Find by id</param>
        public void DeleteUser(int id)
        {
            List<User> users = new List<User>();

            using (UserContext db = new UserContext())
            {
                users = db.Users.ToList();
                var findUser = users.Find(u => u.Id == id);

                db.Users.Remove(findUser);
                db.SaveChanges();
            }
        }
    }
}