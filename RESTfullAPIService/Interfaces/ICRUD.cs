using RESTfullAPIService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfullAPIService.Interfaces
{
    public interface ICRUD
    {  /// <summary>
       /// Create new user
       /// </summary>
       /// <param name="id">Set id</param>
       /// <param name="name">Set username</param>
        public void CreateUser(int id, string name);


        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        public Task<List<User>> GetAllUsers();


        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">Find by id</param>
        /// <returns></returns>
        public User GetUserById(int id);


        /// <summary>
        /// Get user by name, return first find user.
        /// </summary>
        /// <param name="name">Find by name</param>
        /// <returns></returns>
        public User GetUserByName(string name);


        /// <summary>
        /// Edit username by id user
        /// </summary>
        /// <param name="id">Find by id</param>
        /// <param name="name"> For edit </param>
        public void EditUser(int id, string name);

        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="id">Find by id</param>
        public void DeleteUser(int id);
    }
}
