using Microsoft.AspNetCore.Mvc;
using RESTfullAPIService.Interfaces;
using RESTfullAPIService.Models;
using System.Threading.Tasks;

namespace RESTfullAPIService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private ICRUD _icrud;

        public UserController(ICRUD icrud)
        {
            _icrud = icrud;
        }

        // GET: User Get all users
        [HttpGet]
#pragma warning disable CS1998 // В данном асинхронном методе отсутствуют операторы await, поэтому метод будет выполняться синхронно. Воспользуйтесь оператором await для ожидания неблокирующих вызовов API или оператором await Task.Run(...) для выполнения связанных с ЦП заданий в фоновом потоке.
        public async Task<IActionResult> GetAllUser()
#pragma warning restore CS1998 // В данном асинхронном методе отсутствуют операторы await, поэтому метод будет выполняться синхронно. Воспользуйтесь оператором await для ожидания неблокирующих вызовов API или оператором await Task.Run(...) для выполнения связанных с ЦП заданий в фоновом потоке.
        {
            return Ok(_icrud.GetAllUsers());
        }

        // GET: User/{id} Get user by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            return Ok(await _icrud.GetUserById(id));
        }

        // POST: User Create user
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User value)
        {
            return Ok(await _icrud.CreateUser(value.Id, value.Name));
        }

        // PUT: User/{id} Update user by id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User value)
        {
            return Ok(await _icrud.UpdateUser(id, value.Name));
        }

        // DELETE: User/{id} Delete user by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return Ok(await _icrud.DeleteUser(id));
        }
    }
}
