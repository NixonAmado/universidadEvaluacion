using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class ProfesorConfiguration : IEntityTypeConfiguration<Profesor>
{
    public void Configure(EntityTypeBuilder<Profesor> builder)
    {
        builder.ToTable("Profesor");
        
        builder.HasKey(p=> p.Id_profesor);
        builder.Property(p => p.Id_profesor)
                .ValueGeneratedOnAdd();

        builder.HasIndex(p => p.Id_profesor)
                .IsUnique();
        
        builder.HasOne(p => p.Persona)
                .WithMany(p => p.Profesores)
                .HasForeignKey(p => p.Id_persona);

        builder.HasOne(p => p.Departamento)
                    .WithMany(p => p.Profesores)
                    .HasForeignKey(p => p.Id_departamento);
        
    }
}
