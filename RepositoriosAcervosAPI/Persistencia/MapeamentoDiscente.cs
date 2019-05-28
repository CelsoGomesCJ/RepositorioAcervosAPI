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
                           var discente =  MapeieDiscente(dataReader);
                        }
                    }
                }
            }

            return discentes;
        }

        private string ObtenhaConsultaTodosDiscentes()
        {
            throw new NotImplementedException();
        }

        public void RegistreUsuario(Discente discente)
        {
            using (var conexao = Conexao.Instancia.CrieConexao())
            {
                using (var comando = conexao.CreateCommand())
                {
                    comando.CommandText = @"INSERT INTO DISCENTE (NOME, EMAIL, SENHA) VALUES(@NOME, @EMAIL, @SENHA)";

                    comando.Parameters.Add(CrieParametro("@NOME", NpgsqlDbType.Varchar, 250));
                    comando.Parameters.Add(CrieParametro("@EMAIL", NpgsqlDbType.Varchar, 250));
                    comando.Parameters.Add(CrieParametro("@SENHA", NpgsqlDbType.Varchar, 250));
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

        

        public Discente ObtenhaDiscente(string email, string senha)
        {
            using (var conexao = Conexao.Instancia.CrieConexao())
            {
                using (var comando = conexao.CreateCommand())
                {
                    comando.CommandText = ObtenhaConsultaDiscente(email, senha);

                    using (DbDataReader dataReader = comando.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            return MapeieDiscente(dataReader);
                        }
                    }
                }
            }

            return null;

        }

        private Discente MapeieDiscente(DbDataReader dataReader)
        {
            return new Discente()
            {
                Id = dataReader.GetInt32(0),
                nome = dataReader.GetString(1),
                token = dataReader.GetString(2),
                email = dataReader.GetString(3),
            };
        }

        private string ObtenhaConsultaDiscente(string email, string senha)
        {
            return $"SELECT * FROM DISCENTE WHERE EMAIL = '{email}' AND SENHA = '{senha}'";
        }
    }
}
