using RESTfullAPIService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTfullAPIService.Interfaces
{
    /// <summary>
    /// Interface Create Read Update Delete 
    /// </summary>
    public interface ICRUD
    {  
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUsers();


        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">Find by id</param>
        /// <returns></returns>
        public Task<User> GetUserById(int id);

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="id">Set id</param>
        /// <param name="name">Set username</param>
        public Task<User> CreateUser(int id, string name);

        /// <summary>
        /// Edit username by id user
        /// </summary>
        /// <param name="id">Find by id</param>
        /// <param name="name"> For edit </param>
        public Task<User> UpdateUser(int id, string name);

        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="id">Find by id</param>
        public Task<User> DeleteUser(int id);
    }
}
