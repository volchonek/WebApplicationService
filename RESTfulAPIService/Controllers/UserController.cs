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
        ///     Var type fo DI.
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        ///     Dependency inject.
        /// </summary>
        /// <param name="userRepository"></param>
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        ///     Get all users
        /// </summary>
        /// <returns></returns>
        /// <response code="200"> Will return a list of all users or an empty list </response>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userRepository.GetAll());
        }

        /// <summary>
        ///     Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200"> Will return if user found </response>
        /// <response code="404"> Will return if user not found </response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userRepository.GetById(id);
            return user != null ? Ok(user) : NotFound() as IActionResult;
        }

        /// <summary>
        ///     Get user by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <response code="200"> Will return a list of users or an empty list. </response>
        /// <response code="404"> Will return if user not found. </response>
        [HttpGet("name")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByName(string name)
        {
            var user = await _userRepository.GetByName(name);
            return user != null ? Ok(user) : NotFound() as IActionResult;
        }

        /// <summary>
        ///     Create user.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <response code="201"> Will return if create new user. </response>
        /// <response code="400"> Will return if body for create is empty or if user a have invalid parameters. </response>
        /// <response code="409"> Will return if user already exist. </response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Create([FromBody] User value)
        {
            if (value == null)
                return BadRequest("The body is null.");

            if (value.Id.GetType() != typeof(Guid))
                return BadRequest($"The field Id is have invalid type: {value.Id.GetType()}.");

            if (value.Name.GetType() != typeof(string))
                return BadRequest($"The field Name is have invalid type: {value.Id.GetType()}.");

            if (await _userRepository.Create(value))
                return Created("User is create.", value);

            return Conflict($"A user with this Id: {value.Id} already exists.");
        }

        /// <summary>
        ///     Update user.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <response code="200"> Will return if user update. </response>
        /// <response code="400"> Will return if user a have invalid parameters. </response>
        /// <response code="404"> Will return if user not found. </response>
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update([FromBody] User value)
        {
            if (value.Id.GetType() != typeof(Guid))
                return BadRequest($"The field Id is have invalid type: {value.Id.GetType()}.");

            if (value.Name.GetType() != typeof(string))
                return BadRequest($"The field Name is have invalid type: {value.Id.GetType()}.");

            if (value.Id == Guid.Empty)
                return BadRequest("The field Id is empty.");

            if (await _userRepository.Update(value))
                return Ok("User is update.");

            return NotFound();
        }

        /// <summary>
        ///     Delete user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200"> Will return ID if user remote.  </response>
        /// <response code="400"> Will return if user a have invalid parameters. </response>
        /// <response code="404"> Will return if user not found. </response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id.GetType() != typeof(Guid))
                return BadRequest($"The field Id a have invalid type: {id.GetType()}.");

            if (id == Guid.Empty)
                return BadRequest("The field Id is empty.");

            if (await _userRepository.Delete(id))
                return Ok("User is delete.");

            return NotFound();
        }
    }
}