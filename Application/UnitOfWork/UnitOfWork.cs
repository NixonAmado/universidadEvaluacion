using Application.Repository;
using Domain.Interfaces;
using Persistence;

namespace Application.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
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

    public IAsignatura Asignaturas 
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
    public ICursoEscolar CursosEscolares { 
        get
        {
            if (_cursosEscolares == null)
            {
                _cursosEscolares = new CursoEscolarRepository(_context);
            }
            return _cursosEscolares;
        }
     }
    public IDepartamento Departamentos { 
        get
        {
            if (_departamentos == null)
            {
                _departamentos = new DepartamentoRepository(_context);
            }
            return _departamentos;
        }
     
     }
    public IGrado Grados { 
        get
        {
            if (_grados == null)
            {
                _grados = new GradoRepository(_context);
            }
            return _grados;
        }
     
     }
    public IPersona Personas { 
        get
        {
            if (_personas == null)
            {
                _personas = new PersonaRepository(_context);
            }
            return _personas;
        }
             

     }
    public ITipoAsignatura TiposAsignaturas {
        get
        {
            if (_tipoAsignaturas == null)
            {
                _tipoAsignaturas = new TipoAsignaturaRepository(_context);
            }
            return _tipoAsignaturas;
        }
     
     }
    public IProfesor Profesores {
        get
        {
            if (_profesores == null)
            {
                _profesores = new ProfesorRepository(_context);
            }
            return _profesores;
        }
     
    }
    
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}