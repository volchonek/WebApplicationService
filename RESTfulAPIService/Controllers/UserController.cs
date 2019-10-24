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
        /// User Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {           
            return Ok(await _iur.GetAll());    
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByGuid(Guid id)
        {
            return Ok(await _iur.GetByGuid(id));
        }

        /// <summary>
        /// User Create user
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User value)
        {
            return Ok(await _iur.Create(value));
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
                return BadRequest();

            return Ok(await _iur.Update(id, value));
        }

        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _iur.Delete(id));
        }
    }
}
