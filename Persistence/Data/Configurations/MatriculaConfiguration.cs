using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class MatriculaConfiguration : IEntityTypeConfiguration<Matricula>
{
    public void Configure(EntityTypeBuilder<Matricula> builder)
    {
        builder.ToTable("Matricula");
        
        builder.HasKey(p => new { p.Id_alumno, p.Id_Asignatura, p.Id_cursoEscolar });

        builder.HasOne(p => p.Alumno)
                .WithMany(p => p.Matriculas)
                .HasForeignKey(p => p.Id_alumno);

        builder.HasOne(p => p.Asignatura)
                    .WithMany(p => p.Matriculas)
                    .HasForeignKey(p => p.Id_Asignatura);
    
        builder.HasOne(p => p.CursoEscolar)
                    .WithMany(p => p.Matriculas)
                    .HasForeignKey(p => p.Id_cursoEscolar);
        
    }
}
