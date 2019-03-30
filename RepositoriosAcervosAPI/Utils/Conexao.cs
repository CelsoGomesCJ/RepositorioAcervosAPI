using MySql.Data.MySqlClient;
using Npgsql;
using NpgsqlTypes;
using RepositorioAcervosAPI.Models;
using System.Collections.Generic;

namespace RepositoriosAcervosAPI.Utils
{
    public class Conexao
    {
        private NpgsqlConnection Connection;

        public Conexao()
        {
            var connectionString = CrieStringConexao();
            Connection = new NpgsqlConnection(connectionString);
            Connection.Open();
        }

        private static string CrieStringConexao()
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

        public List<string> ObtenhaAcervos()
        {
            using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM acervos", Connection))
            {
                List<string> listaAcervos = new List<string>();

                using (NpgsqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listaAcervos.Add($"{dr["id"]}-{dr["nome"]}");
                    }
                }

                return listaAcervos;
            }
        }

        public void InsiraAcervo(Acervo acervo)
        {
            var cmd = Connection.CreateCommand();
            cmd.CommandText = "INSERT INTO acervos(id, nome) VALUES(@id, @nome)";
            cmd.Parameters.Add("@id", NpgsqlDbType.Integer);
            cmd.Parameters.Add("@id", NpgsqlDbType.Varchar);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public override string ToString()
        {
            return $"Conectado! Data Source: {Connection.DataSource}, Data Base: {Connection.Database}";
        }
    }
}
