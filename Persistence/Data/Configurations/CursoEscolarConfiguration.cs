using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class CursoEscolarConfiguration : IEntityTypeConfiguration<CursoEscolar>
{
    public void Configure(EntityTypeBuilder<CursoEscolar> builder)
    {
        builder.ToTable("Curso_Escolar");
        

        builder.Property(p => p.Anio_inicio)
        .IsRequired();

        builder.Property(p => p.Anio_fin)
        .IsRequired();
    }
}
