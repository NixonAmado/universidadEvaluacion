using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class DepartamentoRepository : GenericRepository<Departamento>, IDepartamento
{
    private readonly UniversityContext _context;

    public DepartamentoRepository(UniversityContext context) : base(context)
    {
        _context = context;
    }
    //10. Devuelve un listado con el nombre de todos los departamentos que tienen profesores que imparten alguna asignatura en el `Grado en Ingeniería Informática (Plan 2015)`.
    public async Task<IEnumerable<Departamento>> GetDepartamentosPorProfesores()
    {
        return await _context.Departamentos
                    .Include(P=> P.Profesores)
                    .Where(d => d.Profesores.Any(p => p.Asignaturas.Any(p => p.Grado.Nombre == "Grado en Ingeniería Informática (Plan 2015)")))
                    .ToListAsync();
    }
    //16. Devuelve un listado con todos los departamentos que tienen alguna asignatura que no se haya impartido en ningún curso escolar. El resultado debe mostrar el nombre del departamento y el nombre de la asignatura que no se haya impartido nunca.
    public async Task<IEnumerable<Object>> GetDepartamentoTieneAsignatura()
    {
        return await _context.Departamentos
                            .Where(p => p.Profesores.Any(p => !p.Asignaturas.Any(p=> p.Matriculas.Any())))
                            .Select(p => new{
                                Departamento = p.Nombre,
                                AsignaturasNoCompartidas = p.Profesores.SelectMany(p => p.Asignaturas.Where(a => a.Matriculas.Any()))
                                .Select(p => p.Nombre)
                                .ToList()
                            })
                            .ToListAsync();        
        }

    // Calcula cuántos profesores hay en cada departamento. El resultado sólo debe mostrar dos columnas, una con el nombre del departamento y otra con el número de profesores que hay en ese departamento. El resultado sólo debe incluir los departamentos que tienen profesores asociados y deberá estar ordenado de mayor a menor por el número de profesores.
    public async Task<IEnumerable<Object>> GetCantProfesoresEnDepartamento()
    {
        return await _context.Departamentos
                            .Where(p => p.Profesores.Count() != 0)
                            .Select(p => new
                            {
                                nombreDepartamento = p.Nombre,
                                ProfesoresAsociados = p.Profesores.Count() 
                            }).OrderByDescending(p => p.ProfesoresAsociados).ToListAsync();
    }
    //Devuelve un listado con todos los departamentos y el número de profesores que hay en cada uno de ellos. Tenga en cuenta que pueden existir departamentos que no tienen profesores asociados. Estos departamentos también tienen que aparecer en el listado.
    public async Task<IEnumerable<Object>>GetCantProfNoAsosiadosDepart()
    {
        return await _context.Departamentos
                            .Select(p => new
                            {
                                nombreDepartamento = p.Nombre,
                                ProfesoresAsociados = p.Profesores.Count() 
                            }).OrderByDescending(p => p.ProfesoresAsociados).ToListAsync();
    }

    //28. Devuelve un listado con los departamentos que no tienen profesores asociados.
    public async Task<IEnumerable<Departamento>> GetDepartamentoSinProfesores()
    {
        return await _context.Departamentos
                            .Where(p => !p.Profesores.Any())
                            .ToListAsync();
    }

//31. Devuelve un listado con todos los departamentos que no han impartido asignaturas en ningún curso escolar.
    public async Task<IEnumerable<Departamento>> GetDepartamentoSinAsigCS()
    {
        return await _context.Departamentos
                            .Where(p => p.Profesores.Any(p => !p.Asignaturas.Any(p => p.Matriculas.Any())))
                            .ToListAsync();
    }

}
















