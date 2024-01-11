using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class EStudenteModel
    {
        [Key]
        public int IdEstudant { get; set; }
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O Campo Nome é obrigatório"),
        Column(TypeName = "varchar(100)")]
        [StringLength(100, ErrorMessage = "O {0} não deve conter menos de {2} caracteres " +
           "e nem mais de {1} caracteres", MinimumLength = 3)]
        public string Nome { get; set; }

        [Display(Name = "Morada")]
        [Required(ErrorMessage = "O Campo Morada é obrigatório"),
        Column(TypeName = "varchar(150)")]
        [StringLength(150, ErrorMessage = "O {0} não deve conter menos de {2} caracteres " +
           "e nem mais de {1} caracteres", MinimumLength = 3)]
        public string Morada { get; set; }

        [Display(Name = "Telefone")]
        [Column(TypeName = "varchar(15)")]
        [StringLength(15, ErrorMessage = "O {0} não deve conter menos de {2} caracteres " +
           "e nem mais de {1} caracteres", MinimumLength = 3)]
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }

        public bool Ativo { get; set; }
        [NotMapped]//para nao mapear na base de dados (nao criar coluna)
        public int Idade { // essa funõa retornará a data de nascimento
            get => (int)Math.Floor((DateTime.Now - DataNascimento).TotalDays / 365.2424);
        }
    }
}
