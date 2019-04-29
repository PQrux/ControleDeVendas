using Microsoft.EntityFrameworkCore;
using ControleDeVendasAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ControleDeVendasAPI.Data
{
    public class ControleDeVendasContext : IdentityDbContext<Usuario, IdentityRole, string>
    {
        public ControleDeVendasContext(DbContextOptions<ControleDeVendasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Venda> Venda { get; set; }
        public virtual DbSet<Produto> Produto { get; set; }
        public virtual DbSet<VendaProduto> VendaProduto { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<VendaProduto>().HasKey(vp => new { vp.Produtoid, vp.Vendaid });
            builder.Entity<VendaProduto>().HasOne<Venda>(vp => vp.Venda).WithMany(v => v.VendaProduto).HasForeignKey(vp => vp.Vendaid);
            builder.Entity<VendaProduto>().HasOne<Produto>(vp => vp.Produto).WithMany(p => p.VendaProduto).HasForeignKey(vp => vp.Produtoid);
            

            base.OnModelCreating(builder);
        }
    }
}
