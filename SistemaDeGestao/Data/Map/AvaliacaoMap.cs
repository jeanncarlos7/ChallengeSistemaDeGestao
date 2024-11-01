using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaDeGestao.Models;

namespace SistemaDeGestao.Data.Map
{
    public class AvaliacaoMap : IEntityTypeConfiguration<AvaliacaoModel>
    {
        public void Configure(EntityTypeBuilder<AvaliacaoModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(225);
            builder.Property(x => x.Descricao).HasMaxLength(1000);
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(255);

            builder.HasOne(x => x.Usuario)
                   .WithMany()
                   .HasForeignKey(x => x.UsuarioId);
        }
    }
}
