using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RepositorioAcervosAPI.Dominio;
using RepositorioAcervosAPI.Models;
using RepositorioAcervosAPI.Persistencia;
using RepositorioAcervosAPI.Utils;
using RepositoriosAcervosAPI.Utils;

namespace RepositoriosAcervosAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        //Realize Login
        [HttpPost]
        [Route("registrarusuario")]
        public RetornoPadrao RegistrarUsuario([FromBody] Discente discente)
        {
            var mapeadorDiscente = new MapeamentoDiscente();
            var retorno = new RetornoPadrao();

            try
            {
                mapeadorDiscente.RegistreUsuario(discente);
                retorno.Codigo = 0;
                retorno.Mensagem = "Usuário registrado com sucesso!";
                retorno.Token = GereTokenDiscente(discente);
            }
            catch(Exception erro)
            {
                retorno.Codigo = 1;
                retorno.Mensagem = $"Falha ao criar usuário {erro.Message}";
            }

            return retorno;
        }


        [HttpGet]
        [Route("realizelogin")]
        public string RealizeLogin([FromBody] Discente discente)
        {
            return $"{discente.nome} chegou aqui na API";
        }

        [HttpGet]
        [Route("obtenhateste")]
        public string ObtenhaInformacoesBancoTeste()
        {
            return new MapeamentoLogin().ObtenhaInformacoesBancoTeste();
        }


        private string GereTokenDiscente(Discente discente)
        {
            return Utilidades.ObtenhaHashSha256($"{discente.nome}{discente.senha}");
        }

    }
}
