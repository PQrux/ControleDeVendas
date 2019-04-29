using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeVendasAPI.Models
{
    public class Venda
    {
        public Venda()
        {
            VendaProduto = new HashSet<VendaProduto>();
        }
        [Key]
        public int id { get; set; }
        [Required]
        public DateTime dataCadastro { get; set; }
        [Required]
        public virtual Usuario Usuario { get; set; }
        [Required]
        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<VendaProduto> VendaProduto { get; set; }
    }
}
