using Microsoft.EntityFrameworkCore;
using RESTfullAPIService.Interfaces;
using RESTfullAPIService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTfullAPIService.Implementations
{
    /// <summary>
    /// Implementation CRUD
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private UserDbContext _db;

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
        /// <param name="guid"> Search user by guid </param>
        /// <returns></returns>
        public async Task<User> GetByGuid(Guid guid) => await _db.Users.FindAsync(guid);


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
        /// <param name="guid"> Guid exist user or guid new user </param>
        /// <param name="user"> New parametr user or new user </param>
        /// <returns></returns>
        public async Task<User> Update(Guid guid, User user)
        {
            if (await _db.Users.FindAsync(guid) != null)
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
        /// <param name="guid"> Guid for delete user </param>
        /// <returns> REturn deleting user </returns>
        public async Task<User> Delete(Guid guid)
        {
            var user = await _db.Users.FindAsync(guid);
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();

            return user;
        }
    }
}
