using RESTfullAPIService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTfullAPIService.Interfaces
{
    /// <summary>
    /// Interface Create Read Update Delete 
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns> Return all searching users </returns>
        public Task<List<User>> GetAll();

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"> Find user by id</param>
        /// <returns> Return searching user </returns>
        public Task<User> GetByGuid(Guid guid);

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="user"> New user </param>
        /// <returns> Return creating user </returns>
        public Task<User> Create(User user);

        /// <summary>
        /// Update or create user
        /// </summary>
        /// <param name="guid"> Guid exist user or guid new user </param>
        /// <param name="user"> New parametr user or new user </param>
        /// <returns> Return updating user </returns>
        public Task<User> Update(Guid guid, User user);

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="guid"> Guid user for delete </param>
        /// <returns> Return deleting user </returns>
        public Task<User> Delete(Guid guid);
    }
}
