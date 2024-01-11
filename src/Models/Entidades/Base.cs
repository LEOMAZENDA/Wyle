using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models.Entidades
{
    public class Base
    {
        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O nome é obrigatório"), Column(TypeName ="varchar(100)")]
        [StringLength(100, ErrorMessage = "O {0} não deve conter menos de {2} caracteres " +
           "e nem mais de {1} caracteres", MinimumLength = 3)]
        public string Nome { get; set; }
        [Display(Name = "Estado")]
        [Column(TypeName = "char(1)")]
        [ReadOnly(true)]
        public string Estado { get; set; }
        [ReadOnly(true)]
        public DateTime? DataRegisto { get; set; }

        [ReadOnly(true)]
        public DateTime? DataUltimaAlteracao { get; set; }
    }
}

