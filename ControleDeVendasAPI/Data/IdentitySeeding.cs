using Microsoft.AspNetCore.Identity;
using System;
using ControleDeVendasAPI.Models;

namespace ControleDeVendasAPI.Data
{
    public class IdentitySeeding
    {
        private readonly ControleDeVendasContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentitySeeding(ControleDeVendasContext context, UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Initialize()
        {
                if (!_roleManager.RoleExistsAsync(Roles.GERENTE).Result)
                {
                    var resultado = _roleManager.CreateAsync(
                        new IdentityRole(Roles.GERENTE)).Result;
                    if (!resultado.Succeeded)
                    {
                        throw new Exception(
                            $"Erro durante a criação da role {Roles.GERENTE}.");
                    }
                }
                if (!_roleManager.RoleExistsAsync(Roles.VENDEDOR).Result)
                {
                    var resultado = _roleManager.CreateAsync(
                        new IdentityRole(Roles.VENDEDOR)).Result;
                    if (!resultado.Succeeded)
                    {
                        throw new Exception(
                            $"Erro durante a criação da role {Roles.VENDEDOR}.");
                    }
                }
                CreateUser(
                    new Usuario()
                    {
                        UserName = "admin",
                        Email = "admin@controledevendas.com.br",
                        EmailConfirmed = true
                    }, "admin", Roles.GERENTE);

                CreateUser(
                    new Usuario()
                    {
                        UserName = "vendedor01",
                        Email = "vendedor01@controledevendas.com.br",
                        EmailConfirmed = true
                    }, "vendedor", Roles.VENDEDOR);
          
        }

        private void CreateUser(
            Usuario user,
            string password,
            string initialRole = null)
        {
            if (_userManager.FindByNameAsync(user.UserName).Result == null)
            {
                var resultado = _userManager
                    .CreateAsync(user, password).Result;

                if (resultado.Succeeded &&
                    !String.IsNullOrWhiteSpace(initialRole))
                {
                    _userManager.AddToRoleAsync(user, initialRole).Wait();
                }
            }
        }
    }
}
