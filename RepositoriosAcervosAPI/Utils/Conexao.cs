using MySql.Data.MySqlClient;
using Npgsql;
using NpgsqlTypes;
using RepositorioAcervosAPI.Models;
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
                Host = "localhost",
                Port = 5432,
                Username = "postgres",
                Password = "postgres",
                Database = "bdlocalacervos",
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
