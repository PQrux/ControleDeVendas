using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ControleDeVendasAPI.Models;
using ControleDeVendasAPI.Configurations;

namespace ControleDeVendasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public JsonResult Get()
        {
            return Json("ola");
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Authorize("Vendedor")]
        public object Get(int id)
        {
            return User.IsInRole("VENDEDOR");
            //return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
