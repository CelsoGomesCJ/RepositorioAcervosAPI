using Npgsql;
using NpgsqlTypes;
using RepositorioAcervosAPI.Dominio;
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

        public void RegistreUsuario(Discente discente)
        {
            using (var conexao = Conexao.Instancia.CrieConexao())
            {
                using (var comando = conexao.CreateCommand())
                {
                    comando.CommandText = @"INSERT INTO DISCENTE (NOME, EMAIL, SENHA) VALUES(@NOME, @EMAIL, @SENHA)";

                    comando.Parameters.Add(CrieParametro("@NOME", NpgsqlDbType.Varchar, 100));
                    comando.Parameters.Add(CrieParametro("@EMAIL", NpgsqlDbType.Varchar, 100));
                    comando.Parameters.Add(CrieParametro("@SENHA", NpgsqlDbType.Varchar, 10));
                    comando.Prepare();
                    comando.Parameters["@NOME"].Value = discente.nome;
                    comando.Parameters["@EMAIL"].Value = discente.email;
                    comando.Parameters["@SENHA"].Value = discente.senha;
                    comando.ExecuteNonQuery();
                }
            }
        }

        private NpgsqlParameter CrieParametro(string campo, NpgsqlDbType tipo, int size)
        {
            return new NpgsqlParameter(campo, tipo, size);
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
