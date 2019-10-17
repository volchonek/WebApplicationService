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
        /// <summary>
        /// Create user in database
        /// </summary>
        /// <param name="id"> Uniquer number for user </param>
        /// <param name="name"> Name for user </param>
        /// <returns></returns>
        public async Task<User> CreateUser(int id, string name)
        {
            User _user;

            await using(UserDbContext _userDbContext = new UserDbContext())
            {
                _user = new User { Id = id, Name = name };
                await _userDbContext.Users.AddAsync(_user);
                await _userDbContext.SaveChangesAsync();
            }

            return _user;
        }

        /// <summary>
        /// Update user in database
        /// </summary>
        /// <param name="id"> Uniquer number to user for search and update user in database </param>
        /// <param name="name"> Name to user </param>
        /// <returns></returns>
        public async Task<User> UpdateUser(int id, string name)
        {
            User _user;

            await using(UserDbContext _db = new UserDbContext())
            {
                _user = await _db.Users.FindAsync(id);
                _user.Name = name;
                _db.Users.Update(_user);
                await _db.SaveChangesAsync();
            }

            return _user;
        }

        /// <summary>
        /// Delete user from database
        /// </summary>
        /// <param name="id"> Uniquer number to user for search and delete user in database </param>
        /// <returns></returns>
        public async Task<User> DeleteUser(int id)
        {
            User _user;

            await using(UserDbContext _db = new UserDbContext())
            {
                _user = await _db.Users.FindAsync(id);
                _db.Users.Remove(_user);
                await _db.SaveChangesAsync();
            }

            return _user;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns> List users </returns>
        public async Task<List<User>> GetAllUsers()
        {
            List<User> _listUsers = new List<User>();

            await using (UserDbContext _userDbContext = new UserDbContext())
            {
                _listUsers = _userDbContext.Users.ToList();
            }

            return _listUsers;
        }

        /// <summary>
        /// Find user by id
        /// </summary>
        /// <param name="id"> For search user by id </param>
        /// <returns></returns>
        public async Task<User> GetUserById(int id)
        {
            User _user;

            await using(UserDbContext _db = new UserDbContext())
            {
                _user = await _db.Users.FindAsync(id);
            }

            return _user;
        }
    }
}
