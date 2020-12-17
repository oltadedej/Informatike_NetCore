using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniversityInformatike.Controllers
{
    [Route("api/University")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        #region Properties
        private readonly ILogger<UniversityController> logger;
        #endregion

        #region   Constructor
        public UniversityController (ILogger<UniversityController> _logger)
        {
            this.logger = _logger ?? throw new ArgumentNullException(nameof(_logger));
        }
        #endregion

        // GET api/<University>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            try
            {
                logger.LogInformation("Sukses");
                return "Ok";
            }
            catch(Exception ex)
            {
                logger.LogError($"Error:{ex.ToString()}");
            }

            return null;
        }

        // POST api/<University>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<University>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<University>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
