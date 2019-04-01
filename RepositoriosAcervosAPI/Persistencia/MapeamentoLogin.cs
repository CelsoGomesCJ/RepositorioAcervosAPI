using RepositorioAcervosAPI.Models;
using RepositoriosAcervosAPI.Utils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace RepositorioAcervosAPI.Persistencia
{
    public class MapeamentoLogin
    {
        //Exemplo
        public bool ValideLogin(string email, string senha)
        {
            using (var conexao = Conexao.Instancia.CrieConexao())
            {
                using (var comando = conexao.CreateCommand())
                {
                    comando.CommandText = ObtenhaConsultaValidacaoLogin();

                    using (DbDataReader dataReader = comando.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private string ObtenhaConsultaValidacaoLogin()
        {
            return @"SELECT * FROM discente";
        }
    }
}
