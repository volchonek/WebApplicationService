using RESTfullAPIService.Interfaces;
using RESTfullAPIService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfullAPIService.Implementations
{
    public class CRUD : ICRUD

    {
        //_dbContext
        public void CreateUser(int id, string name)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public void EditUser(int id, string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAllUsers()
        {
            //=> return await _dbContext.Users.(GetAllAsync);
            throw new NotImplementedException();
        }

        public User GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUserByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
