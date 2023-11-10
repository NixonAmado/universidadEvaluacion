using Application.Repository;
using Domain.Interfaces;
using Persistence;

namespace Application.UnitOfWork;

public class UnitOfWork : IDisposable
{
    private readonly UniversityContext _context;
    private IRol _roles;
    private IUser _users;
    private IUserRol _userole;
    private IAsignatura _asignaturas; 
    private ICursoEscolar _cursosEscolares; 
    private IDepartamento _departamentos; 
    private IGrado _grados; 
    private IPersona _personas; 
    private ITipoAsignatura _tipoAsignaturas; 
    private IProfesor _profesores; 

    public UnitOfWork(UniversityContext context)
    {
        _context = context;
    }
    public IRol Roles
    {
        get
        {
            if (_roles == null)
            {
                _roles = new RolRepository(_context);
            }
            return _roles;
        }
    }
    public IUserRol UserRoles
    {
        get
        {
            if (_userole == null)
            {
                _userole = new UseRolRepository(_context);
            }
            return _userole;
        }
    }

    public IUser Users
    {
        get
        {
            if (_users == null)
            {
                _users = new UserRepository(_context);
            }
            return _users;
        }
    }

    IAsignatura Asignaturas 
    {
        get
        {
            if (_asignaturas == null)
            {
                _asignaturas = new AsignaturaRepository(_context);
            }
            return _asignaturas;
        }
     }
    ICursoEscolar CursosEscolares { 
        get
        {
            if (_cursosEscolares == null)
            {
                _cursosEscolares = new CursoEscolarRepository(_context);
            }
            return _cursosEscolares;
        }
     }
    IDepartamento Departamentos { get; }
    IGrado Grados { get; }
    IPersona Personas { get; }
    ITipoAsignatura TiposAsignaturas { get; }
    IProfesor Profesores { get; }
    
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}