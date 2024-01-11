using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models.Entidades
{
    [Table("Cliente")]
    public class Cliente  : Base
    {
        //public int IdPais { get; set; }
        //[ForeignKey("IdPais")]
        //public Pais Pais { get; set; }
        [Display(Name = "Nº de Telefone")]
        [Column(TypeName = "varchar(15)")]
        [StringLength(15, ErrorMessage = "O {0} não deve conter menos de {2} caracteres " +
         "e nem mais de {1} caracteres", MinimumLength = 3)]
        public string TelefoneNº { get; set; }
        [Display(Name = "E-Mail")]
        [Column(TypeName = "varchar(30)")]
        [StringLength(30, ErrorMessage = "O {0} não deve conter menos de {2} caracteres e nem mais de {1} caracteres", MinimumLength = 3)]
        public string Email { get; set; }
        //public Endereco Endereco { get; set; }
        public ICollection<Endereco> Enderecos { get; set; }
        public ICollection<Venda> Vendas { get; set; }
    }
}
