using InventarioNet.Models;
using InventarioNet.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
/*====================================================================================================== */ 
using Microsoft.IdentityModel.Tokens; //diretriz de identifica��o de token
using System.Text; //diretriz text para identifica��o 
using Microsoft.AspNetCore.Authentication.JwtBearer; //diretriz JwtBearer criada 


namespace InventarioNet
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        //o m�todo de configura��o onde ser� adicionado os servi�os
        public IConfiguration Configuration { get; }  

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)//respons�vel por definir os servi�os que a aplica��o vai usar, incluindo recursos da plataforma como ASP .NET Core MVC e Entity Framework.
        {

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            
            


            //IProdutoRepository e ProdutoRepository sendo registrados
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            services.AddControllers();

           
/*------------------------------------------------------------------------------------------------------------------------ */ 
            ///adiciona sistema o servi�o Swagger >> requerimento swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Place Info Service API",
                    Version = "v1",
                    Description = "Sample service for Learner",
                });
            });
            
            /* 
              - Adicionado o  servi�o de Autentica��o no IServicesCollection informando o seu tipo (bearer),  e, passamos a chave de seguran�a usada quando criamos o token jwt.

               - Habilitamos a valida��o da Audience e do Issuer, obtendo os valores definidos no arquivo de configura��o;

               - Definimos SaveToken igual a true e com isso o token ser� armazenado no contexto HTTP e poderemos acessar o token no controller quando precisarmos;          
             */
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    //valida��o das chaves 
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["Jwt:Audience"],
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };

            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseStatusCodePages(); //c�digo de status das p�ginas quando ocorre erros para ambiente de desenvolvimento

                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();//c�digo de autoriza��o

            app.UseAuthentication(); //c�digo de status das p�ginas quando ocorre erros para ambiente de desenvolvimento
/*------------------------------------------------------------------------------------------------------------------------------- */ 
            ///uso de autentica��o  e autoriza��o do swagger 
            app.UseSwagger();

            app.UseSwaggerUI(options => {
                                            options.RoutePrefix = "swagger";
                                            options.SwaggerEndpoint("/swagger/v1/swagger.json", "PlaceInfo Services"); 
                                        });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
    
}

