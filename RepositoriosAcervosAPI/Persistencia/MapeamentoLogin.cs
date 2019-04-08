using Npgsql;
using RepositorioAcervosAPI.Dominio;
using RepositoriosAcervosAPI.Utils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using NpgsqlTypes;

namespace RepositorioAcervosAPI.Persistencia
{
    public class MapeamentoLogin
    {
        //Exemplo
        public bool UsuarioEhValido(string email, string senha)
        {
            using (var conexao = Conexao.Instancia.CrieConexao())
            {
                using (var comando = conexao.CreateCommand())
                {
                    comando.CommandText = ObtenhaConsultaValidacaoLogin();

                    using (DbDataReader dataReader = comando.ExecuteReader())
                    {
                        return dataReader.Read();
                    }
                }
            }
        }

        public bool RegistreUsuario(Discente discente)
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
            return true;
        }


        private NpgsqlParameter CrieParametro(string campo, NpgsqlDbType tipo, int size)
        {
            return new NpgsqlParameter(campo, tipo, size); 
        }

        private string ObtenhaConsultaValidacaoLogin()
        {
            return @"SELECT * FROM DISCENTE WHERE EMAIL = 'celsogomes22@gmail.com' AND SENHA = 'mamaue22'";
        }
    }
}
