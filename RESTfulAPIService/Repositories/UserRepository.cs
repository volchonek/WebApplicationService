using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RESTfulAPIService.DbContext;
using RESTfulAPIService.Interfaces;
using RESTfulAPIService.Models;

// TODO: database exception handling

namespace RESTfulAPIService.Repositories
{
    /// <summary>
    ///     Implementation User Repository
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// </summary>
        private readonly UserDbContext _userDbContext;

        /// <summary>
        /// </summary>
        /// <param name="userDbContext"></param>
        public UserRepository(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        /// <summary>
        ///     Get all users.
        /// </summary>
        /// <returns> Return list users. </returns>
        public async Task<List<User>> GetAll()
        {
            return await _userDbContext.Users.ToListAsync();
        }

        /// <summary>
        ///     Find user by guid
        /// </summary>
        /// <param name="id"> Guid for search entity user. </param>
        /// <returns> Return user. </returns>
        public async Task<User> GetById(Guid id)
        {
            return await _userDbContext.Users.FindAsync(id);
        }

        /// <summary>
        ///     Find user by id.
        /// </summary>
        /// <param name="name"> Search user by name. </param>
        /// <returns> Return list users. </returns>
        public async Task<List<User>> GetByName(string name)
        {
            return await _userDbContext.Users.Where(value => EF.Functions.ILike(value.Name, $"{name}%")).ToListAsync();
        }

        /// <summary>
        ///     Create user in database.
        /// </summary>
        /// <param name="user"> Entity user. </param>
        /// <returns> Return true/false if user created. </returns>
        public async Task<bool> Create(User user)
        {
            await _userDbContext.Users.AddAsync(user);

            try
            {
                return await _userDbContext.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Update user in database.
        /// </summary>
        /// <param name="user"> New parameters for entity user. </param>
        /// <returns> Return true/false if user updated. </returns>
        public async Task<bool> Update(User user)
        {
            _userDbContext.Users.Update(user);

            try
            {
                return await _userDbContext.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Delete user from database.
        /// </summary>
        /// <param name="id"> Guid for delete entity user. </param>
        /// <returns> Return true/false if user deleted. </returns>
        public async Task<bool> Delete(Guid id)
        {
            _userDbContext.Users.Remove(await _userDbContext.Users.FindAsync(id));

            try
            {
                return await _userDbContext.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            catch
            {
                return false;
            }
        }
    }
}