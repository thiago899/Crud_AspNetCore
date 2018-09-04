using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetoWEBCore.Model
{

    public partial class Cliente
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string cpf { get; set; }
        public DateTime data_nascimento { get; set; }
        public int idade { get; set; }
        public int profissao_id { get; set; }
       
    }

}