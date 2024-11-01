using Microsoft.EntityFrameworkCore;
using SistemaDeGestao.Data.Map;
using SistemaDeGestao.Models;

namespace SistemaDeGestao.Data
{
    public class SistemaTarefasDBContext : DbContext
    {
        // Construtor principal que recebe DbContextOptions e passa para a base
        public SistemaTarefasDBContext(DbContextOptions<SistemaTarefasDBContext> options) : base(options)
        {
        }

        // Definição das entidades
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<TarefaModel> Tarefas { get; set; }
        public DbSet<AvaliacaoModel> Avaliacoes { get; set; }
        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<ClienteModel> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações das entidades e relacionamentos
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new TarefaMap());
            modelBuilder.ApplyConfiguration(new AvaliacaoMap());

            modelBuilder.Entity<AvaliacaoModel>()
                .HasOne(a => a.Usuario)
                .WithMany(u => u.Avaliacoes)
                .HasForeignKey(a => a.UsuarioId)
                .HasConstraintName("FK_AvaliacaoModel_Usuario");

            modelBuilder.Entity<UsuarioModel>().HasKey(u => u.Id);
            modelBuilder.Entity<AvaliacaoModel>().HasKey(a => a.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
