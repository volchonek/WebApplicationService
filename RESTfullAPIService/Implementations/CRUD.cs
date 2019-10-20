using RESTfullAPIService.Interfaces;
using RESTfullAPIService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfullAPIService.Implementations
{
    /// <summary>
    /// Implementation CRUD
    /// </summary>
    public class CRUD : ICRUD
    {
        private UserDbContext _db;

        public CRUD(UserDbContext userDbContext)
        {
            _db = userDbContext;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns> List users </returns>
        public List<User> GetAllUsers()
        {
            List<User> listUsers = new List<User>();

            listUsers = _db.Users.ToList();

            return listUsers;
        }

        /// <summary>
        /// Find user by id
        /// </summary>
        /// <param name="id"> For search user by id </param>
        /// <returns></returns>
        public async Task<User> GetUserById(int id)
        {
            User user;

            user = await _db.Users.FindAsync(id);

            return user;
        }

        /// <summary>
        /// Create user in database
        /// </summary>
        /// <param name="id"> Uniquer number for user </param>
        /// <param name="name"> Name for user </param>
        /// <returns></returns>
        public async Task<User> CreateUser(int id, string name)
        {
            User user;

            user = new User { Id = id, Name = name };
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            return user;
        }

        /// <summary>
        /// Update user in database
        /// </summary>
        /// <param name="id"> Uniquer number to user for search and update user in database </param>
        /// <param name="name"> Name to user </param>
        /// <returns></returns>
        public async Task<User> UpdateUser(int id, string name)
        {
            User user;

            user = await _db.Users.FindAsync(id);
            user.Name = name;
            _db.Users.Update(user);
            await _db.SaveChangesAsync();

            return user;
        }

        /// <summary>
        /// Delete user from database
        /// </summary>
        /// <param name="id"> Uniquer number to user for search and delete user in database </param>
        /// <returns></returns>
        public async Task<User> DeleteUser(int id)
        {
            User user;

            user = await _db.Users.FindAsync(id);
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();

            return user;
        }
    }
}
