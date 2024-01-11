using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entidades
{
    [Table("Venda")]
    public class Venda
    {   [Key]   
        public int IdVenda { get; set; }
        public DateTime? DataVenda { get; set; }
        public DateTime? DataEntrega { get; set; }
        public decimal? ValorTotal { get; set; }
        public int IdCliente { get; set; }
        [ForeignKey("IdCliente")]
        public Cliente Cliente { get; set; }
        public string Operador  { get; set; }
        //[ForeignKey("IdFuncionario")]
        //public Funcionario Funcionario  { get; set; }
        public Endereco EnderecoEntrega { get; set; }
        [Display(Name = "Estado")]
        [Column(TypeName = "char(1)")]
        public string Estado { get; set; }
        public ICollection<ItemVenda> ItensVenda { get; set; }
    }
}
