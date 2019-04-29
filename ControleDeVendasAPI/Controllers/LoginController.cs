using ControleDeVendasAPI.Configurations;
using ControleDeVendasAPI.Models;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace ControleDeVendasAPI.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {

        public readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        public readonly SigningConfigurations _signingConfigurations;
        public readonly TokenConfigurations _tokenConfigurations;

        public LoginController(
            [FromServices]UserManager<Usuario> userManager,
            [FromServices]SignInManager<Usuario> signInManager,
            [FromServices]SigningConfigurations signingConfigurations,
            [FromServices]TokenConfigurations tokenConfigurations
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
        }
        [AllowAnonymous]
        [HttpPost]
        public object Post(HttpRequest request, [FromBody]User usuario)
        {
            bool credenciaisValidas = false;
            
            object responseObj = new
            {
                authenticated = false,
                message = "Falha ao autenticar"
            };

            if (usuario != null && !String.IsNullOrWhiteSpace(usuario.UserID))
            {
                // Verifica a existência do usuário nas tabelas do
                // ASP.NET Core Identity
                var userIdentity = _userManager
                    .FindByNameAsync(usuario.UserID).Result;
                if (userIdentity != null)
                {
                    // Efetua o login com base no Id do usuário e sua senha
                    var resultadoLogin = _signInManager
                        .CheckPasswordSignInAsync(userIdentity, usuario.Password, false)
                        .Result;
                    credenciaisValidas = resultadoLogin.Succeeded;
        
                    if (credenciaisValidas)
                    {
                        var userRoles = _userManager.GetRolesAsync(userIdentity).Result;

                        ClaimsIdentity identity = new ClaimsIdentity(
                            new GenericIdentity(usuario.UserID, "Login"),
                            new[] {
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                                new Claim(JwtRegisteredClaimNames.UniqueName, usuario.UserID)
                            }
                        );
                        foreach (var item in userRoles)
                        {
                            identity.AddClaim(new Claim(ClaimTypes.Role, item));
                        }
                        DateTime dataCriacao = DateTime.Now;
                        DateTime dataExpiracao = dataCriacao +
                            TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

                        var handler = new JwtSecurityTokenHandler();
                        var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                        {
                            Issuer = _tokenConfigurations.Issuer,
                            Audience = _tokenConfigurations.Audience,
                            SigningCredentials = _signingConfigurations.SigningCredentials,
                            Subject = identity,
                            NotBefore = dataCriacao,
                            Expires = dataExpiracao,

                        });
                        var token = handler.WriteToken(securityToken);
                        responseObj = new
                        {
                            authenticated = true,
                            created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                            expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                            accessToken = token,
                            message = "OK"
                        };
                    }
                }
            }
            return responseObj;
            
        }
    }
}
