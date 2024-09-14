
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SistemaDeGestao.Data;
using SistemaDeGestao.Repositorios;
using SistemaDeGestao.Services;
using SistemaDeGestao.Settings;

namespace SistemaDeGestao
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            // Carregar as configurações do MongoDbSettings a partir do appsettings.json
            builder.Services.Configure<MongoDbSettings>(
                builder.Configuration.GetSection("MongoDbSettings"));

            // Registrar o serviço MongoDB como Singleton
            builder.Services.AddSingleton<MongoDbService>();


            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                options.JsonSerializerOptions.MaxDepth = 64;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            IServiceCollection serviceCollection = builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<SistemaTarefasDBContext>(
                Options =>
                {
                    Options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"));
                });

            builder.Services.AddScoped<Repositorios.Interfaces.IUsuarioRepositorio, UsuarioRepositorio>();
            builder.Services.AddScoped<Repositorios.Interfaces.ITarefaRepositorio, TarefaRepositorio>();
            builder.Services.AddScoped<Repositorios.Interfaces.IAvaliacaoRepositorio, AvaliacaoRepositorio>();

            // Program.cs - Swagger Configuration
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "API Sistema de Gestão",
                    Description = "Documentação da API Sistema de Gestão",
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void AddDbContext<T>(Func<object, object> value)
        {
            throw new NotImplementedException();
        }
    }
}
