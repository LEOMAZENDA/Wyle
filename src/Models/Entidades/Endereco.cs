using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entidades
{
    [Owned, Table("Endereco")] //Owned é uma propriedade do EntityFrameworkCore faz com que essa entidade pertença a uma outra entidade
    public class Endereco 
    {
        [Key] 
        public int IdEndereco { get; set; }       
        [Display(Name = "Província")]
        [Required(ErrorMessage = "O campo província é obrigatório"),
           Column(TypeName = "varchar(100)")]
        [StringLength(50, ErrorMessage = "O {0} não deve conter menos de {2} caracteres " +
          "e nem mais de {1} caracteres", MinimumLength = 3)]
        public string Provincia { get; set; }

        [Display(Name = "Município")]
        [Required(ErrorMessage = "O campo município é obrigatório"),
          Column(TypeName = "varchar(50)")]
        [StringLength(50, ErrorMessage = "O {0} não deve conter menos de {2} caracteres " +
         "e nem mais de {1} caracteres", MinimumLength = 3)]
        public string Municipio { get; set; }

        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "O campo bairro é obrigatório"),
          Column(TypeName = "varchar(50)")]
        [StringLength(50, ErrorMessage = "O {0} não deve conter menos de {2} caracteres " +
         "e nem mais de {1} caracteres", MinimumLength = 3)]
        public string Bairro { get; set; }

        [Display(Name = "Rua")]
        [Required(ErrorMessage = "O campo rua é obrigatório"),
          Column(TypeName = "varchar(50)")]
        [StringLength(50, ErrorMessage = "O {0} não deve conter menos de {2} caracteres " +
         "e nem mais de {1} caracteres", MinimumLength = 3)]
        public string Rua { get; set; }

        [Display(Name = "Nº da casa")]
        [Column(TypeName = "varchar(50)")]
        [StringLength(50, ErrorMessage = "O {0} não deve conter menos de {2} caracteres " +
         "e nem mais de {1} caracteres", MinimumLength = 3)]
        public string CasaNº { get; set; }

        [Display(Name = "Referência")]
        [Column(TypeName = "varchar(50)")]
        [StringLength(50, ErrorMessage = "O {0} não deve conter menos de {2} caracteres " +
        "e nem mais de {1} caracteres", MinimumLength = 3)]
        public string Referencia { get; set; }       
        public bool Selecionado { get; set; }
        [NotMapped]
        public string EnderecoComplecto 
        { get 
            {
                return $"{Provincia} - {Municipio}, Bairro: {Bairro} - Rua: {Rua}, Casa nº {CasaNº}, Referência: { Referencia }";
            }
        }
    }
}
