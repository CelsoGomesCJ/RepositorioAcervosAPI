using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositorioAcervosAPI.Models
{
    public class RetornoPadrao : RetornoAbstrato
    {
        public dynamic Result { get; set; }
        public static RetornoPadrao CrieFalhaLogin()
        {
            return new RetornoPadrao
            {
                Codigo = 1,
                Mensagem = "Nao autorizado"
            };
        }

        public static RetornoPadrao CrieSucessoLogin()
        {
            return new RetornoPadrao
            {
                Codigo = 0,
                Mensagem = "Autenticado com sucesso!"
            };
        }
    }
}
