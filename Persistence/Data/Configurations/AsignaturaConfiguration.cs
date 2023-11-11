using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class AsignaturaConfiguration : IEntityTypeConfiguration<Asignatura>
{
    public void Configure(EntityTypeBuilder<Asignatura> builder)
    {
        builder.ToTable("Asignatura");
        
        builder.Property(p => p.Nombre)
                    .HasColumnType("varchar(100)")
                    .IsRequired();

        builder.Property(p => p.Creditos)
                    .HasColumnType("FLOAT")
                    .IsRequired();

        builder.Property(p => p.Curso)
                    .HasColumnType("TINYINT");


        builder.Property(p => p.Cuatrimestre)
                    .HasColumnType("TINYINT")
                    .IsRequired();     


        
        builder.HasOne(p => p.Profesor)
                    .WithMany(p => p.Asignaturas)
                    .HasForeignKey(p => p.Id_profesor);
        
        builder.HasOne(p => p.Grado)
                    .WithMany(p => p.Asignaturas)
                    .HasForeignKey(p => p.Id_Grado);
        
        builder.Property(e => e.Tipo)
                    .HasColumnName("tipo")
                    .IsRequired()
                    .HasAnnotation("EnumDisplayFormat", "{0}")
                    .HasMaxLength(15)
                    .HasConversion<string>()
                    .IsUnicode(false);
    }
}














































































