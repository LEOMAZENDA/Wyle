using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entidades
{
    [Table("Producto")]
    public class Producto : Base
    {       
        //[DataType(DataType.ImageUrl)]
        //[Display(Name = "Imagem")]
        //public string UrlImagem { get; set; }
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }
        [Display(Name = "Estóque")]
        public int Estoque { get; set; }
        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Este Campo é Obrigatorio")]
        //[Range(1, int.MaxValue, ErrorMessage = "Categoria invalida")]
        public int IdCategoria { get; set; }
        [ForeignKey("IdCategoria")]
        public Categoria Categoria { get; set; }
        [Display(Name = "Observação")]
        [Column(TypeName = "varchar(200)")]
        [StringLength(200, ErrorMessage = "O {0} não deve conter menos de {2} caracteres " +
        "e nem mais de {1} caracteres", MinimumLength = 3)]
        public string Obs { get; set; }

    }
}
