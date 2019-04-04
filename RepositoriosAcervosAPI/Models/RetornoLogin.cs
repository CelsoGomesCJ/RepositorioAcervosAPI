using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositorioAcervosAPI.Models
{
    public class RetornoLogin : RetornoAbstrato
    {
        public string Token { get; set; }
    }
}
