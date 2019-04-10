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

        public string ObtenhaInformacoesBancoTeste()
        {
            var retorno = string.Empty;

            using (var conexao = Conexao.Instancia.CrieConexao())
            {
                using (var comando = conexao.CreateCommand())
                {
                    comando.CommandText = ObtenhaConsultaInformacoesBancoTeste();

                    using (DbDataReader dataReader = comando.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var nome = dataReader.GetString(0);
                            var allow = dataReader.GetBoolean(1);

                            retorno = $"Database: {nome} - Permite Conexões: {allow}";
                        }
                    }
                }
            }

            return retorno;
        }

        private string ObtenhaConsultaValidacaoLogin()
        {
            return @"SELECT * FROM DISCENTE WHERE EMAIL = 'celsogomes22@gmail.com' AND SENHA = 'mamaue22'";
        }

        private string ObtenhaConsultaInformacoesBancoTeste()
        {
            return @"SELECT datname, 
                     datallowconn FROM pg_database
                     where datname like '%d4ji4tdursu951%'";
        }
    }
}
