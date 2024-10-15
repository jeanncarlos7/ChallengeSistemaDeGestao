using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SistemaDeGestao.Data;
using SistemaDeGestao.Repositorios;
using SistemaDeGestao.Repositorios.Interfaces;
using SistemaDeGestao.Services;
using SistemaDeGestao.Settings;
using System.Reflection;

namespace SistemaDeGestao
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Carregar configurações do MongoDbSettings a partir do appsettings.json
            builder.Services.Configure<MongoDbSettings>(
                builder.Configuration.GetSection("MongoDbSettings"));

            // Registrar serviços para integração com MongoDB e outros
            builder.Services.AddSingleton<MongoDbService>();
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<RecommendationService>();

            // Configuração para o HttpClient com Timeout e Retry
            builder.Services.AddHttpClient<AuthService>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .ConfigureHttpClient(client =>
                {
                    client.Timeout = TimeSpan.FromSeconds(30); // Timeout de 30 segundos
                });

            // Configuração do contexto de banco de dados Oracle
            builder.Services.AddDbContext<SistemaTarefasDBContext>(options =>
                options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

            // Configuração dos repositórios
            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            builder.Services.AddScoped<ITarefaRepositorio, TarefaRepositorio>();
            builder.Services.AddScoped<IAvaliacaoRepositorio, AvaliacaoRepositorio>();
            builder.Services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();

            // Configuração de controladores e JSON
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                options.JsonSerializerOptions.MaxDepth = 64;
            });

            // Configuração do Swagger com Segurança JWT
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API Sistema de Gestão",
                    Version = "v1",
                    Description = "Documentação da API Sistema de Gestão"
                });

                // Definição de segurança para JWT
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Por favor, insira o token JWT com o prefixo 'Bearer' [Ex: Bearer {token}]",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                // Adiciona comentários XML no Swagger
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            var app = builder.Build();

            // Configuração do pipeline HTTP
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SistemaDeGestao v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRouting();

            app.MapControllers();

            app.Run();
        }
    }
}
