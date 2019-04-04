using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositorioAcervosAPI.Dominio
{
    public class Discente
    {
        public long Id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public string token { get; set; }
    }
}
