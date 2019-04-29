using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeVendasAPI.Models
{
    public class Usuario : IdentityUser
    {
        public virtual ICollection<Cliente> Cliente { get; set; }
        public virtual ICollection<Produto> Produto { get; set; }
        public virtual ICollection<Venda> Venda { get; set; }
    }
}
