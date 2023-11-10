using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class TipoAsignaturaConfiguration : IEntityTypeConfiguration<TipoAsignatura>
{
    public void Configure(EntityTypeBuilder<TipoAsignatura> builder)
    {
        builder.ToTable("TipoAsignatura");
        
        builder.Property(p => p.Descripcion)
                    .HasColumnType("varchar(100)")
                    .IsRequired();
    }
}
