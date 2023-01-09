using InventarioNet.Models;
using InventarioNet.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
/*====================================================================================================== */ 
using Microsoft.IdentityModel.Tokens; //diretriz de identificação de token
using System.Text; //diretriz text para identificação 
using Microsoft.AspNetCore.Authentication.JwtBearer; //diretriz JwtBearer criada 


namespace InventarioNet
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        //o método de configuração onde será adicionado os serviços
        public IConfiguration Configuration { get; }  

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)//responsável por definir os serviços que a aplicação vai usar, incluindo recursos da plataforma como ASP .NET Core MVC e Entity Framework.
        {

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            
            


            //IProdutoRepository e ProdutoRepository sendo registrados
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            services.AddControllers();

           
/*------------------------------------------------------------------------------------------------------------------------ */ 
            ///adiciona sistema o serviço Swagger >> requerimento swagger
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
              - Adicionado o  serviço de Autenticação no IServicesCollection informando o seu tipo (bearer),  e, passamos a chave de segurança usada quando criamos o token jwt.

               - Habilitamos a validação da Audience e do Issuer, obtendo os valores definidos no arquivo de configuração;

               - Definimos SaveToken igual a true e com isso o token será armazenado no contexto HTTP e poderemos acessar o token no controller quando precisarmos;          
             */
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    //validação das chaves 
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
                app.UseStatusCodePages(); //código de status das páginas quando ocorre erros para ambiente de desenvolvimento

                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();//código de autorização

            app.UseAuthentication(); //código de status das páginas quando ocorre erros para ambiente de desenvolvimento
/*------------------------------------------------------------------------------------------------------------------------------- */ 
            ///uso de autenticação  e autorização do swagger 
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

