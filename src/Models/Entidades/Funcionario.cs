using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entidades
{
    [Table("Funcionario")]
    public class Funcionario : Base
    {      
      
        [Display(Name = "Login")]
        [Required(ErrorMessage = "O campo E-Mail é obrigatório"), Column(TypeName = "varchar(25)")]
        [StringLength(25, ErrorMessage = "O {0} não deve conter menos de {2} caracteres " +
            "e nem mais de {1} caracteres", MinimumLength = 3)]
        public string Email { get; set; }

        [Column(TypeName = "varchar(25)")]
        [StringLength(25, ErrorMessage = "O {0} não deve conter menos de {2} caracteres " +
            "e nem mais de {1} caracteres", MinimumLength = 3)]
        public string Senha { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DataNascimento { get; set; }
        [NotMapped]//para nao mapear na base de dados (nao criar coluna)
        public int Idade {
            get => (int)Math.Floor((DateTime.Now - DataNascimento).TotalDays / 365.2424);
        }
        public int IdFuncao { get; set; }
        [ForeignKey("IdFuncao")]
        public Funcao Funcao {get; set;}
    }
}
