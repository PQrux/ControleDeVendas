using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeVendasAPI.Models
{
    public class Produto
    {
        public Produto()
        {
            VendaProduto = new HashSet<VendaProduto>();
        }
        [Key]
        public int id { get; set; }
        [StringLength(255)]
        [Required(AllowEmptyStrings = false)]
        public string nome { get; set; }
        [Required]
        [DefaultValue(1)]
        public int quantidadeEstoque { get; set; }
        [Required]
        public DateTime dataCadastro { get; set; }

        [Required]
        public virtual Usuario Usuario { get; set; }

        public virtual ICollection<VendaProduto> VendaProduto { get; set; }
    }
}
