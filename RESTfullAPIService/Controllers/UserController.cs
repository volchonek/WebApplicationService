using Microsoft.AspNetCore.Mvc;
using RESTfullAPIService.Interfaces;
using RESTfullAPIService.Models;
using System;
using System.Threading.Tasks;

namespace RESTfullAPIService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserRepository _iur;

        public UserController(IUserRepository iur)
        {
            _iur = iur;
        }

        // GET: User Get all users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _iur.GetAll());    
        }

        // GET: User/{id} Get user by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByGuid(Guid guid)
        {
            return Ok(await _iur.GetByGuid(guid));
        }

        // POST: User Create user
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User value)
        {
            return Ok(await _iur.Create(value));
        }

        // PUT: User/{id} Update user by id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid guid, [FromBody] User value)
        {
            return Ok(await _iur.Update(guid, value));
        }

        // DELETE: User/{id} Delete user by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            return Ok(await _iur.Delete(guid));
        }
    }
}
