using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;

public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
{
    public void Configure(EntityTypeBuilder<Persona> builder)
    {
        builder.ToTable("Persona");
        
        builder.Property(p => p.Nombre)
                    .HasColumnType("varchar(25)")
                    .IsRequired();
        
        builder.Property(p => p.Nif)
                    .HasColumnType("varchar(9)")
                    .IsRequired();    
    
        builder.Property(p => p.Apellido1)
                    .HasColumnType("varchar(50)")
                    .IsRequired();
        
        builder.Property(p => p.Apellido2)
                    .HasColumnType("varchar(50)");
        
        builder.Property(p => p.Ciudad)
                    .HasColumnType("varchar(25)")
                    .IsRequired();

        builder.Property(p => p.Direccion)
                    .HasColumnType("varchar(50)")
                    .IsRequired();

        builder.Property(p => p.Telefono)
                    .HasColumnType("varchar(9)");


        builder.Property(p => p.Fecha_nacimiento)
                    .HasColumnType("Date")
                    .IsRequired();
        
        builder.Property(e => e.Sexo)
                .HasColumnName("sexo")
                .IsRequired()
                .HasAnnotation("EnumDisplayFormat", "{0}")
                .HasMaxLength(15)
                .HasConversion<string>()
                .IsUnicode(false);
                
        builder.Property(p => p.Tipo)
                .HasColumnName("tipo")
                .IsRequired()
                .HasAnnotation("EnumDisplayFormat", "{0}")
                .HasMaxLength(15)
                .HasConversion<string>()
                .IsUnicode(false);
                
    }
}
