using CRMWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMWeb.Infrastructure.Configurations;

public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
{
    public void Configure(EntityTypeBuilder<Endereco> builder)
    {
        builder.ToTable("Endereco");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Logradouro)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Numero)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Complemento)
            .HasMaxLength(100);

        builder.Property(x => x.Bairro)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Cidade)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Uf)
            .HasMaxLength(2)
            .IsRequired();

        builder.Property(x => x.Cep)
            .HasMaxLength(8)
            .IsRequired();

        builder.Property(x => x.Principal)
            .IsRequired();

        builder.HasIndex(x => x.ClienteId);
    }
}