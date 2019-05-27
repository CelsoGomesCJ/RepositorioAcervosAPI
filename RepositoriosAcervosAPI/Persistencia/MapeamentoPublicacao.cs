using Npgsql;
using NpgsqlTypes;
using RepositorioAcervosAPI.Dominio;
using RepositoriosAcervosAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositorioAcervosAPI.Persistencia
{
    public class MapeamentoPublicacao
    {
        public void realizePublicacao(Publicacao publicacao)
        {
            using (var conexao = Conexao.Instancia.CrieConexao())
            {
                using (var comando = conexao.CreateCommand())
                {
                    comando.CommandText = ObtenhaComandoDeInsercaoPublicacao();

                    comando.Parameters.Add(CrieParametro("@TITULO", NpgsqlDbType.Varchar));
                    comando.Parameters.Add(CrieParametro("@SUBTITULO", NpgsqlDbType.Varchar));
                    comando.Parameters.Add(CrieParametro("@PALAVRACHAVE", NpgsqlDbType.Varchar));
                    comando.Parameters.Add(CrieParametro("@RESUMO", NpgsqlDbType.Varchar));
                    comando.Parameters.Add(CrieParametro("@AUTORES", NpgsqlDbType.Varchar));
                    comando.Parameters.Add(CrieParametro("@DOCUMENTO", NpgsqlDbType.Bytea));
                    comando.Parameters.Add(CrieParametro("@DATA_PUBLICACAO", NpgsqlDbType.Date));
                    comando.Parameters.Add(CrieParametro("@DATA_REMOCAO", NpgsqlDbType.Date));
                    comando.Parameters.Add(CrieParametro("@ID_DISCENTE", NpgsqlDbType.Integer));
                    comando.Prepare();
                    comando.Parameters["@TITULO"].Value = publicacao.titulo;
                    comando.Parameters["@SUBTITULO"].Value = publicacao.subtitulo;
                    comando.Parameters["@PALAVRACHAVE"].Value = publicacao.palavrachave;
                    comando.Parameters["@RESUMO"].Value = publicacao.resumo;
                    comando.Parameters["@AUTORES"].Value = publicacao.autores;
                    comando.Parameters["@DOCUMENTO"].Value = publicacao.documento;
                    comando.Parameters["@DATA_PUBLICACAO"].Value = DateTime.Now;
                    comando.Parameters["@DATA_REMOCAO"].Value = null;
                    comando.Parameters["@ID_DISCENTE"].Value = publicacao.discenteid;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public List<Publicacao> ObtenhaPublicacoesPeloId(int publicacao)
        {
            var publicacoesDoUsuario = new List<Publicacao>();

            using (var conexao = Conexao.Instancia.CrieConexao())
            {
                conexao.Open();

                using (var comando = conexao.CreateCommand())
                {
                    comando.CommandText = ObtenhaConsultaPublicacoesDoUsuario();
                    comando.Parameters.Add(CrieParametroWithValue("@ID_DISCENTE", publicacao));

                    using (var dr = comando.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            publicacoesDoUsuario.Add(new Publicacao {
                                Id = dr.GetInt64(0),
                                titulo = dr.GetString(1),
                                subtitulo = dr.GetString(2),
                                palavrachave = dr.GetString(3),
                                resumo = dr.GetString(4),
                                autores = dr.GetString(5),
                                documento = (byte[])dr.GetValue(6),
                                discenteid = dr.GetInt64(9)
                            });
                        }
                    }
                }
            }

            return publicacoesDoUsuario;
        }

        private static string ObtenhaConsultaPublicacoesDoUsuario()
        {
            return @"SELECT * FROM PUBLICACAO WHERE ID_DISCENTE = @ID_DISCENTE";
        }

        private NpgsqlParameter CrieParametro(string campo, NpgsqlDbType tipo)
        {
            return new NpgsqlParameter(campo, tipo);
        }

        private NpgsqlParameter CrieParametroWithValue(string campo, object value)
        {
            return new NpgsqlParameter(campo, value);
        }

        private string ObtenhaComandoDeInsercaoPublicacao()
        {
            return @"INSERT INTO public.publicacao(TITULO, SUBTITULO, PALAVRACHAVE, RESUMO, AUTORES, DOCUMENTO, DATA_PUBLICACAO, DATA_REMOCAO, ID_DISCENTE)
	                VALUES (@TITULO, @SUBTITULO, @PALAVRACHAVE, @RESUMO, @AUTORES, @DOCUMENTO, @DATA_PUBLICACAO, @DATA_REMOCAO, @ID_DISCENTE)";
        }
    }
}
