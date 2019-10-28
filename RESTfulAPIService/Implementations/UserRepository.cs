using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RESTfulAPIService.DbContext;
using RESTfulAPIService.Interfaces;
using RESTfulAPIService.Models;

// TODO: database exception handling

namespace RESTfulAPIService.Implementations
{
    /// <summary>
    ///     Implementation User Repository
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
        ///     Get all users
        /// </summary>
        /// <returns> Return list users </returns>
        public async Task<List<User>> GetAll()
        {
            return await _db.Users.ToListAsync();
        }

        /// <summary>
        ///     Find user by guid
        /// </summary>
        /// <param name="id"> Guid for search entity user </param>
        /// <returns> Return user </returns>
        public async Task<User> GetByGuid(Guid id)
        {
            return await _db.Users.FindAsync(id);
        }

        /// <summary>
        ///     Find user by id
        /// </summary>
        /// <param name="name"> Search user by name </param>
        /// <returns> Return list users </returns>
        public async Task<List<User>> GetByName(string name)
        {
            return await _db.Users.Where(n => n.Name == name).ToListAsync();
        }

        /// <summary>
        ///     Create user in database
        /// </summary>
        /// <param name="user"> Entity user </param>
        /// <returns> Return true/false if user created </returns>
        public async Task<bool> Create(User user)
        {
            try
            {
                await _db.Users.AddAsync(user);
                return await _db.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        // TODO: update user
        /// <summary>
        ///     Update user in database
        /// </summary>
        /// <param name="user"> New parameters for entity user </param>
        /// <returns> Return true/false if user updated </returns>
        public async Task<bool> Update(User user)
        {
            try
            {
                if (await _db.Users.FindAsync(user.Id) != null)
                    _db.Users.Update(user);

                return await _db.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Delete user from database
        /// </summary>
        /// <param name="id"> Guid for delete entity user </param>
        /// <returns> Return true/false if user deleted </returns>
        public async Task<bool> Delete(Guid id)
        {
            try
            {
                _db.Users.Remove(await _db.Users.FindAsync(id));
                return await _db.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}