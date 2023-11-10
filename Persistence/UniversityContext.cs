using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class UniversityContext : DbContext
{
    public UniversityContext(DbContextOptions options) : base(options)
    { }
    public DbSet<User> Users { get; set; }
    public DbSet<Rol> Roles { get; set; }
    public DbSet<UserRol> UserRoles { get; set; }
    public DbSet<Persona> Personas { get; set; }
    public DbSet<Profesor> Profesores { get; set; }
    public DbSet<Departamento> Departamentos { get; set; }
    public DbSet<Grado> Grados { get; set; }
    public DbSet<TipoAsignatura> TipoAsignaturas { get; set; }
    public DbSet<Asignatura> Asignaturas { get; set; }
    public DbSet<Matricula> Matriculas { get; set; }
    public DbSet<CursoEscolar> CursosEscolares { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}