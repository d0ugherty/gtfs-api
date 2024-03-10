using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GtfsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GtfsController : ControllerBase
    {
        // GET: api/<GtfsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<GtfsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<GtfsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<GtfsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GtfsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
