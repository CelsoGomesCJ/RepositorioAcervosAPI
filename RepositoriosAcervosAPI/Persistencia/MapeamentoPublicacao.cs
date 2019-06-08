using Npgsql;
using NpgsqlTypes;
using RepositorioAcervosAPI.Dominio;
using RepositoriosAcervosAPI.Utils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RepositorioAcervosAPI.Persistencia
{
    public class MapeamentoPublicacao
    {

        //Depois ajusta para pegar a data de publicação de acordo com o LOCALTIMESTAMP
        public Publicacao realizePublicacao(Publicacao publicacao)
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
                    comando.Parameters.Add(CrieParametro("@ID_DISCENTE", NpgsqlDbType.Integer));
                    comando.Prepare();
                    comando.Parameters["@TITULO"].Value = publicacao.titulo;
                    comando.Parameters["@SUBTITULO"].Value = publicacao.subtitulo;
                    comando.Parameters["@PALAVRACHAVE"].Value = publicacao.palavrachave;
                    comando.Parameters["@RESUMO"].Value = publicacao.resumo;
                    comando.Parameters["@AUTORES"].Value = publicacao.autores;
                    comando.Parameters["@DOCUMENTO"].Value = publicacao.documento;
                    comando.Parameters["@DATA_PUBLICACAO"].Value = DateTime.Now;
                    comando.Parameters["@ID_DISCENTE"].Value = publicacao.discenteid;
                    comando.ExecuteNonQuery();
                }
            }

            //Voltar aqui e fazer o código transacionado

            using (var conexao = Conexao.Instancia.CrieConexao())
            {
                using (var comando = conexao.CreateCommand())
                {
                    comando.CommandText = @"SELECT MAX(ID) FROM publicacao";

                    using (var dr = comando.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            publicacao.Id = dr.GetInt64(0);
                        }
                    }
                }
            }

            return publicacao;
        }

        public List<Publicacao> ObtenhaPublicacoesPeloId(int idUsuario)
        {
            var publicacoesDoUsuario = new List<Publicacao>();

            using (var conexao = Conexao.Instancia.CrieConexao())
            {
                using (var comando = conexao.CreateCommand())
                {
                    comando.CommandText = ObtenhaConsultaPublicacoesDoUsuario();
                    comando.Parameters.Add(CrieParametroWithValue("@ID_DISCENTE", idUsuario));

                    using (var dr = comando.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var publicacao = MapeiePublicacao(dr);
                            publicacoesDoUsuario.Add(publicacao);
                        }
                    }
                }
            }

            return publicacoesDoUsuario;
        }

        public void DeletePublicacaoPeloId(long idPublicacao)
        {
            using (var conexao = Conexao.Instancia.CrieConexao())
            {
                using (var comando = conexao.CreateCommand())
                {
                    comando.CommandText = ObtenhaConsultaDeletePublicacaoPeloId();
                    comando.Parameters.Add(CrieParametroWithValue("@ID", idPublicacao));
                    comando.ExecuteNonQuery();
                }
            }
        }

        private Publicacao MapeiePublicacao(DbDataReader dr)
        {
            var publicacao = new Publicacao();
            publicacao.Id = dr.GetInt64(0);
            publicacao.titulo = dr.GetString(1);
            publicacao.subtitulo = dr.GetString(2);
            publicacao.palavrachave = dr.GetString(3);
            publicacao.resumo = dr.GetString(4);
            publicacao.autores = dr.GetString(5);
            publicacao.discenteid = dr.GetInt64(9);
            publicacao.documento = (byte[])dr.GetValue(6);

            return publicacao;
        }

        private static string ObtenhaConsultaPublicacoesDoUsuario()
        {
            return @"SELECT * FROM PUBLICACAO WHERE ID_DISCENTE = @ID_DISCENTE";
        }

        private string ObtenhaConsultaDeletePublicacaoPeloId()
        {
            return @"DELETE FROM PUBLICACAO WHERE ID = @ID";
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
            return @"INSERT INTO public.publicacao(TITULO, SUBTITULO, PALAVRACHAVE, RESUMO, AUTORES, DOCUMENTO, DATA_PUBLICACAO, ID_DISCENTE)
	                VALUES (@TITULO, @SUBTITULO, @PALAVRACHAVE, @RESUMO, @AUTORES, @DOCUMENTO, @DATA_PUBLICACAO, @ID_DISCENTE)";
        }
    }
}
