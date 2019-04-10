using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositorioAcervosAPI.Models
{
    public class RetornoPadrao : RetornoAbstrato
    {
        public string Token { get; set; }
        public dynamic Result { get; set; }
        public static RetornoPadrao CrieFalhaLogin()
        {
            return new RetornoPadrao
            {
                Codigo = 1,
                Mensagem = "Nao autorizado"
            };
        }
    }
}
