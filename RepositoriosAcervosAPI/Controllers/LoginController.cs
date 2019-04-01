using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RepositorioAcervosAPI.Models;
using RepositorioAcervosAPI.Persistencia;
using RepositoriosAcervosAPI.Utils;

namespace RepositoriosAcervosAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //Deploy Heroku
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
             return new string[] { "Value123" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return $"Value: {id}";
        }

        //RealizeLogin
        // POST api/values
        [HttpPost]
        public ActionResult<string> Post([FromBody] Discente discente)
        {
            MapeamentoLogin mapeador = new MapeamentoLogin();
            mapeador.ValideLogin(discente.email, discente.senha);

            return $"{ "Validado Com Sucesso" }";
        }

        //CrieConta()
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
