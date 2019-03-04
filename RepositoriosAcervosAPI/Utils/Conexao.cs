using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoriosAcervosAPI.Utils
{
    public class Conexao
    {
        private MySqlConnection Connection;

        public Conexao()
        {
            var connectionString = CrieStringConexao();
            Connection = new MySqlConnection(connectionString);
            Connection.Open();
        }

        private static string CrieStringConexao()
        {
            var sbConnectionString = new MySqlConnectionStringBuilder()
            {
                Server = "mysqlserveracervo.mysql.database.azure.com",
                Database = "repositorioacervos",
                UserID = "master@mysqlserveracervo",
                Password = "Sysdba09",
            };

            return sbConnectionString.ToString();
        }

        public List<string> ObtenhaAcervos()
        {
            using (MySqlCommand command = new MySqlCommand("SELECT * FROM acervos", Connection))
            {
                List<string> listaAcervos = new List<string>();

                using (MySqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        listaAcervos.Add($"{dr["id"]}-{dr["nome"]}-{dr["sigla"]}");
                    }
                }

                return listaAcervos;
            }
        }
    }
}
