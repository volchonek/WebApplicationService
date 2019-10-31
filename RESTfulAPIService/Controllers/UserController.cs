using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTfulAPIService.Interfaces;
using RESTfulAPIService.Models;

namespace RESTfulAPIService.Controllers
{
    /// <summary>
    /// </summary>
    [Produces("application/json")]
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
        ///     Get all users
        /// </summary>
        /// <returns></returns>
        /// <response code="200"> Will return a list of all users or an empty list </response>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAll() => Ok(await _iur.GetAll());
        
        /// <summary>
        ///     Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200"> Will return if user found </response>
        /// <response code="400"> Will return if user not found </response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetByGuid(Guid id) => await _iur.GetByGuid(id) == null ? Ok() : BadRequest() as IActionResult;

        /// <summary>
        ///     Get user by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <response code="200"> Will return a list of users or an empty list </response>
        [HttpGet("name")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetByName(string name) => Ok(await _iur.GetByName(name));
        
        /// <summary>
        ///     Create user
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <response code="201"> Will return if create new user </response>
        /// <response code="409"> Will return if user already exist </response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Create([FromBody] User value)
        {
            if (await _iur.Create(value))
                return Created("User create", value);

            return Conflict($"A user with this Id: {value.Id} already exists.");
        }

        /// <summary>
        ///     Update user
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <response code="200"> Will return if user update </response>
        /// <response code="400"> Will return field Id is empty or 00000000-0000-0000-0000-000000000000 </response>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] User value)
        {
            if (value.Id.ToString() == string.Empty || value.Id == Guid.Empty)
                return BadRequest("The field Id is empty");

            if (await _iur.Update(value))
                return Ok("User update");

            return BadRequest();
        }

        /// <summary>
        ///     Delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200"> Will return ID if user remote  </response>
        /// <response code="400"> Will return if user not found </response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (await _iur.Delete(id))
                return Ok($"User with Id: {id} was delete");

            return BadRequest();
        }
    }
}