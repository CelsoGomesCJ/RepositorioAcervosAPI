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
        [HttpGet]
        [HttpPost]
        public RetornoLogin Post([FromBody] Discente discente)
        {
            var retorno = new RetornoLogin();

            MapeamentoLogin mapeador = new MapeamentoLogin();

            if (mapeador.UsuarioEhValido(discente.email, discente.senha))
            {
                retorno.Codigo = 0;
                retorno.Mensagem = "Usuário autenticado";
                retorno.Token = GereTokenDiscente(discente);
            }
            else
            {
                retorno.Codigo = 1;
                retorno.Mensagem = "Usuário inválido";
            }

            return retorno;
        }

        private string GereTokenDiscente(Discente discente)
        {
            return Utilidades.ObtenhaHashSha256($"{discente.nome}{discente.senha}");
        }

        ////CrieConta
        //[HttpPost]
        //public ActionResult<string> Post(string, email, senha)
        //{
        //    MapeamentoLogin mapeador = new MapeamentoLogin();
        //    mapeador.RegistreUsuario(discente);

        //    return $"{ "Registrado Com Sucesso"}";
        //}

    }
}
