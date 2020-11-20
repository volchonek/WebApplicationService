using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RESTfulAPIService.Models;

namespace RESTfulAPIService.Interfaces
{
    /// <summary>
    ///     Interface fpr Users
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        ///     Get all users
        /// </summary>
        /// <returns> Return all found users </returns>
        Task<List<User>> GetAll();

        /// <summary>
        ///     Get user by id
        /// </summary>
        /// <param name="id"> Guid for searching user </param>
        /// <returns> Found user will return </returns>
        Task<User> GetById(Guid id);

        /// <summary>
        ///     Get users by name
        /// </summary>
        /// <param name="name"> Name for searching user</param>
        /// <returns> Return the list of the user found </returns>
        Task<List<User>> GetByName(string name);

        /// <summary>
        ///     Create new user
        /// </summary>
        /// <param name="user"> New user </param>
        /// <returns> Return create user </returns>
        Task<bool> Create(User user);

        /// <summary>
        ///     Update or create user
        /// </summary>
        /// <param name="user"> New parameters for user </param>
        /// <returns> Return update user </returns>
        Task<bool> Update(User user);

        /// <summary>
        ///     Delete user
        /// </summary>
        /// <param name="id"> Guid for delete user </param>
        /// <returns> Return deleting user </returns>
        Task<bool> Delete(Guid id);
    }
}