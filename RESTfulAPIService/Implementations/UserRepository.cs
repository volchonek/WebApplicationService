using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RESTfulAPIService.DbContext;
using RESTfulAPIService.Interfaces;
using RESTfulAPIService.Models;

namespace RESTfulAPIService.Implementations
{
    /// <summary>
    /// Implementation User Repository
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// </summary>
        private readonly UserDbContext _db;
        
        /// <summary>
        /// </summary>
        /// <param name="userDbContext"></param>
        public UserRepository(UserDbContext userDbContext)
        {
            _db = userDbContext;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns> Return list users </returns>
        public async Task<List<User>> GetAll() => await _db.Users.ToListAsync();

        /// <summary>
        /// Find user by guid
        /// </summary>
        /// <param name="id"> Search user by guid </param>
        /// <returns></returns>
        public async Task<User> GetByGuid(Guid id) => await _db.Users.FindAsync(id);


        /// <summary>
        /// Create user in database
        /// </summary>
        /// <param name="user"> Create user </param>
        /// <returns> Return creating user </returns>
        public async Task<User> Create(User user)
        { 
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            return user;
        }

        /// <summary>
        /// Update user in database
        /// </summary>
        /// <param name="id"> Guid exist user or guid new user </param>
        /// <param name="user"> New parameters user or new user </param>
        /// <returns></returns>
        public async Task<User> Update(Guid id, User user)
        {
            if (await _db.Users.FindAsync(id) != null)
            {
                _db.Users.Update(user);
            }
            else 
            {
               await _db.Users.AddAsync(user);
            }
         
            await _db.SaveChangesAsync();

            return user;
        }

        /// <summary>
        /// Delete user from database
        /// </summary>
        /// <param name="id"> Guid for delete user </param>
        /// <returns> Return deleting user </returns>
        public async Task<User> Delete(Guid id)
        {
            var user = await _db.Users.FindAsync(id);
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();

            return user;
        }
    }
}
