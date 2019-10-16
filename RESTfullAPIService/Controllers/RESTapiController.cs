using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RESTfullAPIService.Controllers
{
    [ApiController]
    [Route("rest-api/[controller]")]
    public class RESTapiController : ControllerBase
    {
        readonly CRUDController crud = new CRUDController();

        // GET: api/User
        [HttpGet]
        public IEnumerable<string> Get()
        { 

            crud.GetAllUsers();
            return new string[] { "user1", "user2" };
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "user_id";
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
