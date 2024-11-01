using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SistemaDeGestao.Data;
using SistemaDeGestao.Repositorios;
using SistemaDeGestao.Repositorios.Interfaces;
using SistemaDeGestao.Services;
using SistemaDeGestao.Services.Interfaces;
using SistemaDeGestao.Settings;
using System.Reflection;

namespace SistemaDeGestao
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Carregar configura��es do MongoDbSettings a partir do appsettings.json
            builder.Services.Configure<MongoDbSettings>(
                builder.Configuration.GetSection("MongoDbSettings"));

            // Registrar servi�os para integra��o com MongoDB e outros
            builder.Services.AddSingleton<MongoDbService>();
            builder.Services.AddSingleton<RecommendationService>();

            // Configura��o para o HttpClient com Timeout e Retry
            builder.Services.AddHttpClient<ViaCepService>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .ConfigureHttpClient(client =>
                {
                    client.Timeout = TimeSpan.FromSeconds(30); // Timeout de 30 segundos
                });

            // Configura��o do contexto de banco de dados Oracle
            builder.Services.AddDbContext<SistemaTarefasDBContext>(options =>
                options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

            // Configura��o dos reposit�rios
            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            builder.Services.AddScoped<ITarefaRepositorio, TarefaRepositorio>();
            builder.Services.AddScoped<IAvaliacaoRepositorio, AvaliacaoRepositorio>();
            builder.Services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
            builder.Services.AddScoped<IViaCepService, ViaCepService>();

            // Configura��o de controladores e JSON
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                options.JsonSerializerOptions.MaxDepth = 64;
            });



            // Configura��o do Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API Sistema de Gest�o",
                    Version = "v1",
                    Description = "Documenta��o da API Sistema de Gest�o"
                });

                // Adiciona coment�rios XML no Swagger
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            var app = builder.Build();

            // Configura��o do pipeline HTTP
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SistemaDeGestao v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.MapControllers();

            app.Run();
        }
    }
}
