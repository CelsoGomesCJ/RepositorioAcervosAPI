using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RepositoriosAcervosAPI.Utils;

namespace RepositoriosAcervosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcervosController : ControllerBase
    {

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            //Conexao conexao = new Conexao();
            //var retorno = conexao.ObtenhaAcervos();

            //return new string[] {
            //    conexao.ToString(),
            //    retorno.First(),
            //};


            return new string[] {
                "value teste",
                "value teste33",
            };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            //var teste = $"{id}";

            //return new string[] { teste };

            return $"Value: {id}";
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
