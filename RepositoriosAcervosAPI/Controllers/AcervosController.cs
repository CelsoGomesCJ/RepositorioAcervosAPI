using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RepositorioAcervosAPI.Models;
using RepositoriosAcervosAPI.Utils;

namespace RepositoriosAcervosAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AcervosController : ControllerBase
    {
        //Deploy Heroku
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            try
            {
                Conexao conexao = new Conexao();
                var retorno = conexao.ObtenhaAcervos();

                return new string[] {
                    conexao.ToString(),
                    retorno.First(),
                };
            }
            catch (Exception ex)
            {
                return new string[] { ex.Message };
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return $"Value: {id}";
        }

        // POST api/values
        [HttpPost]
        public ActionResult<string> Post([FromBody] Acervo acervo)
        {
            try
            {
                Conexao conexao = new Conexao();
                conexao.InsiraAcervo(acervo);

                return $"Acervo: {acervo.Descricao} incluído com sucesso!";
            }
            catch (Exception ex)
            {
                return $"{ ex.Message }";
            }
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
