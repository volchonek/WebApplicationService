using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTfulAPIService.Interfaces;
using RESTfulAPIService.Models;

namespace RESTfulAPIService.Controllers
{
    /// <summary>
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// </summary>
        private readonly IUserRepository _iur;

        /// <summary>
        /// </summary>
        /// <param name="iur"></param>
        public UserController(IUserRepository iur)
        {
            _iur = iur;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _iur.GetAll()); // 200

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByGuid(Guid id) => Ok(await _iur.GetByGuid(id)); // 200

        /// <summary>
        /// Get user by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("name")]
        public async Task<IActionResult> GetByName(string name) => Ok( await _iur.GetByName(name)); // 200
        
        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User value)
        {
            // TODO: как перехватить сообщение при пустом body 
            
            if (await _iur.Create(value))
            {
                return Created($"User create {value.Id}", value); // 201
            }
            else
            {
                return Conflict($"A user with this {value.Id} already exists."); // 409
            }
        }

        /// <summary>
        /// Update user by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] User value)
        {

            if (id != value.Id) 
                return BadRequest($"The {id} does not match the {value.Id} in the body"); // 400

            if (await _iur.Update(id, value))
            {
                return Created($"User {id} update or create successful", value); // 201
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (await _iur.Delete(id))
            {
                return  Ok($"User delete {id}"); // 200
            }
            else
            {
                return NoContent(); // 204
            }
        }
    }
}
