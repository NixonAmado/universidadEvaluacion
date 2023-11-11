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
    // public async Task<IEnumerable<Departamento>> GetDepartamentoTieneAsignatura()
    // {
    //     return await _context.Departamentos
    //                         .Include(p => p.Profesores)
    //                        // .ThenInclude(p => p.Where(p => p.Asignaturas.Any(p => p.Matriculas.Any(p => p.CursoEscolar == "1"))));
    // }


}