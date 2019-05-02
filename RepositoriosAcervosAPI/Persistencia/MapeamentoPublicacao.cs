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
                    comando.CommandText = obtenhaComandoDeInsercaoPublicacao();

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

        private NpgsqlParameter CrieParametro(string campo, NpgsqlDbType tipo)
        {
            return new NpgsqlParameter(campo, tipo);
        }

        private string obtenhaComandoDeInsercaoPublicacao()
        {
            return @"INSERT INTO public.publicacao(TITULO, SUBTITULO, PALAVRACHAVE, RESUMO, AUTORES, DOCUMENTO, DATA_PUBLICACAO, DATA_REMOCAO, ID_DISCENTE)
	                VALUES (@TITULO, @SUBTITULO, @PALAVRACHAVE, @RESUMO, @AUTORES, @DOCUMENTO, @DATA_PUBLICACAO, @DATA_REMOCAO, @ID_DISCENTE)";
        }
    }
}
