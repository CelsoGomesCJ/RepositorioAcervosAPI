using Npgsql;
using RepositorioAcervosAPI.Models;
using RepositoriosAcervosAPI.Utils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace RepositorioAcervosAPI.Persistencia
{
    public class MapeamentoDiscente
    {
        public List<Discente> ObtenhaDiscentes()
        {
            var discentes = new List<Discente>();

            using (var conexao = Conexao.Instancia.CrieConexao())
            {
                using (var comando = conexao.CreateCommand())
                {
                    comando.CommandText = ObtenhaConsultaTodosDiscentes();

                    using (DbDataReader dataReader = comando.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                           var discente =  MapeieDiscente();
                        }
                    }
                }
            }

            return discentes;
        }

        private Discente MapeieDiscente()
        {
            return new Discente();
        }

        private string ObtenhaConsultaTodosDiscentes()
        {
            throw new NotImplementedException();
        }
    }
}
