using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models.Entidades
{
    [Table("ItemVenda")]
    public class ItemVenda
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //Pra nao ser gerado automaticamente
        public int IdVenda { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]//Pra nao ser gerado automaticamente
        public int IdProducto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        [ForeignKey("IdVenda")]
        public Venda Venda { get; set; }
        [ForeignKey("IdProducto")]
        public Producto Producto { get; set; }

        [NotMapped]
        public decimal ValorItem { get => this.Quantidade * this.ValorItem;}          
    }
}
