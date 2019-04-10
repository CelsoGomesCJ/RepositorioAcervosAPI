using MySql.Data.MySqlClient;
using Npgsql;
using NpgsqlTypes;
using RepositorioAcervosAPI.Dominio;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace RepositoriosAcervosAPI.Utils
{
    public class Conexao
    {
        public static Conexao Instancia = new Conexao();
        private readonly string _stringConexao;

        public Conexao()
        {
            _stringConexao = CrieStringConexao();
        }

        private string CrieStringConexao()
        {
            var sbConnectionString = new NpgsqlConnectionStringBuilder()
            {
                Host = "ec2-54-221-201-212.compute-1.amazonaws.com",
                Port = 5432,
                Username = "tqwsgczicxjcvd",
                Password = "e9526b4c0f4a848330f3bc6cc0b73270e75b5c576b78e4b04b462c661deae03e",
                Database = "d4ji4tdursu951",
                SslMode = SslMode.Require,
                TrustServerCertificate = true
            };

            return sbConnectionString.ToString();
        }

        public DbConnection CrieConexao()
        {
            try
            {
                var connection = new NpgsqlConnection(_stringConexao);
                connection.Open();
                return connection;
            }
            catch (Exception message)
            {
                throw new Exception($"Falha ao abrir conexão com banco de dados. {message}");
            }
        }
    }
}
