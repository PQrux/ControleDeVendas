using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace ControleDeVendasAPI.Models
{
    public class Cliente
    {
        public Cliente()
        {
            Venda = new HashSet<Venda>();
        }
        [Key]
        public int id { get; set; }
        [StringLength(255)]
        [Required(AllowEmptyStrings = false)]
        public string primeiroNome { get; set; }
        [StringLength(255)]
        [Required(AllowEmptyStrings = false)]
        public string sobrenome { get; set; }
        [StringLength(11)]
        [Required(AllowEmptyStrings = false)]
        public string CPF { get; set; }
        [Required]
        public DateTime dataCadastro { get; set; }

        [Required]
        public virtual Usuario usuario { get; set; }

        public virtual ICollection<Venda> Venda { get; set; }
    }
}
