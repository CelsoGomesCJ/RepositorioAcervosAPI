using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositorioAcervosAPI.Models
{
    public class Acervo
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
    }
}
