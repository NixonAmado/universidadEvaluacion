using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class AsignaturaRepository : GenericRepository<Asignatura>, IAsignatura
{
    private readonly UniversityContext _context;

    public AsignaturaRepository(UniversityContext context) : base(context)
    {
        _context = context;
    }
    //Devuelve el listado de las asignaturas que se imparten en el X cuatrimestre, en el x curso del grado que tiene el identificador `x`.
    public async Task<IEnumerable<Asignatura>> GetAsignaturasPorGrado(int cuatr,int curso,int grado)
    {
        return await _context.Asignaturas
                    .Where(p => p.Cuatrimestre == cuatr && p.Curso == curso && p.Id_Grado == grado)
                    .ToListAsync();
    }
    //7. Devuelve un listado con todas las asignaturas ofertadas en el `Grado en Ingeniería Informática (Plan 2015)`.
    public async Task<IEnumerable<Asignatura>> GetAsignaturasOfertadasGrado()
    {
        return await _context.Asignaturas
                    .Where(p => p.Grado.Nombre == "Grado en Ingeniería Informática (Plan 2015)") 
                    .ToListAsync();
    }
    //15. Devuelve un listado con las asignaturas que no tienen un profesor asignado.
    public async Task<IEnumerable<Asignatura>> GetAsignaturasSinProfesor()
    {
        return await _context.Asignaturas
                            .Where(p =>  p.Id_profesor == null)
                            .ToListAsync();
    }
}
