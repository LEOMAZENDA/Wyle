using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Entidades
{
    public class Funcao : Base
    {
        public ICollection<Funcionario > Funcionarios { get; set; }
    }
}
