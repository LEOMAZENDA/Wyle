using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Models.Entidades
{
    [Table("Categoria")]
    public class Categoria : Base
    {
        public ICollection<Producto> Productos { get; set; }
    }
}
