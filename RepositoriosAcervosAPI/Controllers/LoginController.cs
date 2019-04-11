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

            var hashSenha = Utilidades.ObtenhaHashSha256(discente.senha);
            discente.senha = hashSenha;

            try
            {
                mapeadorDiscente.RegistreUsuario(discente);
                retorno.Codigo = 0;
                retorno.Mensagem = "Usuário registrado com sucesso!";
            }
            catch(Exception erro)
            {
                retorno.Codigo = 1;
                retorno.Mensagem = $"Falha ao criar usuário";
            }

            return retorno;
        }


        [HttpPost]
        [Route("realizelogin")]
        public RetornoPadrao RealizeLogin([FromBody] Discente discente)
        {
            var mapeadorLogin = new MapeamentoLogin();
            var mapeadorDiscente = new MapeamentoDiscente();

            var hashSenha = Utilidades.ObtenhaHashSha256(discente.senha);
            discente.senha = hashSenha;

            if (mapeadorLogin.UsuarioEhValido(discente.email, discente.senha))
            {
                var retorno = RetornoPadrao.CrieSucessoLogin();
                retorno.Result = mapeadorDiscente.ObtenhaDiscente(discente.email, discente.senha);
                return retorno;
            }

            return RetornoPadrao.CrieFalhaLogin();
           
        }

        [HttpGet]
        [Route("obtenhateste")]
        public string ObtenhaInformacoesBancoTeste()
        {
            return new MapeamentoLogin().ObtenhaInformacoesBancoTeste();
        }

    }
}
