using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTfullAPIService.Interfaces;
using RESTfullAPIService.Models;

namespace RESTfullAPIService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RESTapiController : ControllerBase
    {
        readonly CRUDController crud = new CRUDController();
        private ICRUD _iCrud;

        public RESTapiController(ICRUD iCRUD)
        {
            _iCrud = iCRUD;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _iCrud.GetAllUsers());
            //return new string[] { "user1", "user2" };
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "user_id";
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] User value)
        {
            //if()
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
