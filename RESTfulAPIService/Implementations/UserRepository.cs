using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RESTfulAPIService.DbContext;
using RESTfulAPIService.Interfaces;
using RESTfulAPIService.Models;

// TODO: подумать над обработкой ошибок  при работе с базой данных

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
        /// <returns> Return user </returns>
        public async Task<User> GetByGuid(Guid id) => await _db.Users.FindAsync(id);
        
        /// <summary>
        /// Find user by id
        /// </summary>
        /// <param name="name"> Search user by name </param>
        /// <returns> Return list users </returns>
        public async Task<List<User>> GetByName(string name)
        {
            var users = await _db.Users.ToListAsync();
            List<User> fusers = new List<User>();
            
            foreach (var user in users)
            {
                if (user.Name == name)
                    fusers.Add(user);
            }

            return fusers;
        }


        /// <summary>
        /// Create user in database
        /// </summary>
        /// <param name="user"> Create user </param>
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

        /// <summary>
        /// Update user in database
        /// </summary>
        /// <param name="id"> Guid exist user or guid new user </param>
        /// <param name="user"> New parameters user or new user </param>
        /// <returns> Return true/false if user created or updated </returns>
        public async Task<bool> Update(Guid id, User user)
        {
            if (await _db.Users.FindAsync(id) != null)
            {
                _db.Users.Update(user);
            }
            else 
            {
               await _db.Users.AddAsync(user);
            }
         
            return await _db.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Delete user from database
        /// </summary>
        /// <param name="id"> Guid for delete user </param>
        /// <returns> Return true/false if user deleted </returns>
        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var user = await _db.Users.FindAsync(id);
                _db.Users.Remove(user);
                return await _db.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
