using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTfullAPIService.Interfaces;
using RESTfullAPIService.Models;

namespace RESTfullAPIService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RESTapiController : ControllerBase
    {
        private ICRUD _icrud;

        public RESTapiController(ICRUD icrud)
        {
            _icrud = icrud;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            return Ok( _icrud.GetAllUsers());
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> GetUserById(int id)
        {
            return Ok(await _icrud.GetUserById(id));
        } 

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User value)
        {
           return Ok(await _icrud.CreateUser(value.Id, value.Name));
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User value)
        {
            return Ok(await _icrud.UpdateUser(value.Id, value.Name));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return Ok(await _icrud.DeleteUser(id));
        }
    }
}
