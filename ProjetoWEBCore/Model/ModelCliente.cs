using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoWEBCore.Model
{
    public class ModelCliente
    {
        public int id { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string cpf { get; set; }
        public DateTime date { get; set; }
        public int age { get; set; }
        public string profession { get; set; }
    }
}
