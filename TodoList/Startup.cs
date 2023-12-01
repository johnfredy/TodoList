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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TodoList.Context;
using TodoList.Models;
using TodoList.Services;
using TodoList.Services.Contracts;

namespace TodoList
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDBContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));
            services.AddControllers().AddNewtonsoftJson(options => 
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            services.AddControllers();
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo() //Swashbuckle.AspNetCore.Swagger.info() se usa en versiones anteriores
                    {
                        Title = "Ejemplo de swagger",
                        Description = "Esta es una documentación de swagger, aquí tambien puede ir información necesaria para utilizar el Api, etc..."
                    }
                );
                config.IncludeXmlComments(xmlPath);
            });

            //Configurando automapper
            services.AddAutoMapper(typeof(Startup));

            //estos valores los obtenemos de nuestro appsettings
            var issuer = Configuration["AuthenticationSettings:Issuer"];
            var audience = Configuration["AuthenticationSettings:Audience"];
            var signinKey = Configuration["AuthenticationSettings:SigningKey"];
            //autenticacion
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {
                    //options.Audience = audience;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        //ValidateIssuer = true,
                        ValidIssuer = issuer,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(signinKey)),

                        ValidateIssuer = false,
                        ValidateAudience = false,
                        //ValidateLifetime = true,
                        //ValidateIssuerSigningKey = true,
                        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWTKey"])),
                        ClockSkew = TimeSpan.Zero
                    };
                }
            );

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDBContext>()
                .AddDefaultTokenProviders();

            // JWT
            services.AddAuthorization(options =>
                options.DefaultPolicy =
                new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build()
            );

            //Habilitacion de CORS
            services.AddCors(options =>
               options.AddDefaultPolicy(x => x.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader()
              .WithExposedHeaders(new string[] { "cantidadTotalRegistros" }))
            );

            services.AddScoped(typeof(IAuthService), typeof(AuthService));//inyectando servicio
            services.AddSession();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "donet5 v1"));
            }

            PrepareDb.Population(app);
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Configuración de middleware SWAGGER
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "API SWAGGER!");
                config.RoutePrefix = ""; //para evitar problemas con la ruta en la que se lanza la aplicación al correr el proyecto
            }
            );

            //configiracion Jwt
            app.UseAuthentication();
            app.UseSession();
        }
    }
}
