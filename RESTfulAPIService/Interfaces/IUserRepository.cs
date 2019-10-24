using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RESTfulAPIService.Models;

namespace RESTfulAPIService.Interfaces
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
        Task<List<User>> GetAll();

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"> Find user by id</param>
        /// <returns> Return searching user </returns>
        Task<User> GetByGuid(Guid id);

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="user"> New user </param>
        /// <returns> Return creating user </returns>
        Task<User> Create(User user);

        /// <summary>
        /// Update or create user
        /// </summary>
        /// <param name="id"> Guid exist user or guid new user </param>
        /// <param name="user"> New parameters user or new user </param>
        /// <returns> Return updating user </returns>
        Task<User> Update(Guid id, User user);

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Return deleting user </returns>
        Task<User> Delete(Guid id);
    }
}
