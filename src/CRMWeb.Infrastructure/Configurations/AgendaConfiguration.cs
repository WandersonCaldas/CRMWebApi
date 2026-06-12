using CRMWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMWeb.Infrastructure.Configurations;

public class AgendaConfiguration : IEntityTypeConfiguration<Agenda>
{
    public void Configure(EntityTypeBuilder<Agenda> builder)
    {
        builder.ToTable("Agenda");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Titulo)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Descricao)
            .HasMaxLength(1000);

        builder.Property(x => x.DataInicio)
            .IsRequired();

        builder.Property(x => x.DataFim)
            .IsRequired();

        builder.Property(x => x.DiaTodo)
            .IsRequired();

        builder.Property(x => x.Ativo)
            .IsRequired();

        builder.HasOne(x => x.Cliente)
            .WithMany(x => x.Agendas)
            .HasForeignKey(x => x.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Contato)
            .WithMany(x => x.Agendas)
            .HasForeignKey(x => x.ContatoId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(x => x.ClienteId);

        builder.HasIndex(x => x.ContatoId);
    }
}