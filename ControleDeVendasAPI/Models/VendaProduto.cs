using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeVendasAPI.Models
{
    public class VendaProduto
    {
        [Key]
        [Column(Order = 0)]
        public int Vendaid { get; set; }

        public virtual Venda Venda { get; set; }

        [Key]
        [Column(Order = 1)]
        public int Produtoid { get; set; }

        public virtual Produto Produto { get; set; }
    }
}
