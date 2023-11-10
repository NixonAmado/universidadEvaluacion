namespace Domain.Interfaces;

public interface IUnitOfWork
{
    IRol Roles { get; }
    IUser Users { get; }
    IUserRol UserRoles { get; }
    IAsignatura Asignaturas { get; }
    ICursoEscolar CursosEscolares { get; }
    IDepartamento Departamentos { get; }
    IGrado Grados { get; }
    IPersona Personas { get; }
    ITipoAsignatura TiposAsignaturas { get; }
    IProfesor Profesores { get; }
    
    Task<int> SaveAsync();
}