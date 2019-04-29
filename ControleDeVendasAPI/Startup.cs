using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleDeVendasAPI.Configurations;
using ControleDeVendasAPI.Data;
using ControleDeVendasAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ControleDeVendasAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<ControleDeVendasContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("ControleDeVendasContext")
                //,builder => builder.MigrationsAssembly("Data/ControleDeVendasContext")
            ));

            // Ativando a utilização do ASP.NET Identity, a fim de
            // permitir a recuperação de seus objetos via injeção de
            // dependências
            services.AddIdentity<Usuario, IdentityRole>()
                .AddEntityFrameworkStores<ControleDeVendasContext>()
                .AddDefaultTokenProviders()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddDefaultUI();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                Configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;
                
                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                paramsValidation.ClockSkew = TimeSpan.Zero;
                //paramsValidation.NameClaimType = "name";
                //paramsValidation.RoleClaimType = "role";
            });

            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Vendedor", policy =>
                    policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAssertion(e => { return e.User.IsInRole("VENDEDOR"); })
                    .RequireAuthenticatedUser().Build());
                auth.AddPolicy("Gerente", policy =>
                    policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAssertion(e => { return e.User.IsInRole("GERENTE"); })
                    .RequireAuthenticatedUser().Build());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ControleDeVendasContext context, UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            }

            new IdentitySeeding(context, userManager, roleManager).Initialize();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
