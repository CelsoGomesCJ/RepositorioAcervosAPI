using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositorioAcervosAPI.Dominio
{
    public class Publicacao
    {
        public long Id { get; set; }
        public string titulo { get; set; }
        public string subtitulo { get; set; }
        public string palavrachave { get; set; }
        public string resumo { get; set; }
        public string autores { get; set; }
        public byte[] documento { get; set; }
        public long discenteid { get; set; }
    }
}
