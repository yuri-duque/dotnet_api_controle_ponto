using System;
using Microsoft.AspNetCore.Mvc;
using Service.Service;

namespace Controle_Ponto_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaController : ControllerBase
    {
        private Service_Dia _service = new Service_Dia();

        // GET: api/Dia
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_service.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Dia/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            return Ok("value");
        }

        // POST: api/Dia
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Dia/5
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
