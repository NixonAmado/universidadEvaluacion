using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
using static Domain.Entities.Asignatura;

namespace Application.Repository;
public class GradoRepository : GenericRepository<Grado>, IGrado
{
    private readonly UniversityContext _context;

    public GradoRepository(UniversityContext context) : base(context)
    {
        _context = context;
    }
    //21.Devuelve un listado con el nombre de todos los grados existentes en la base de datos y el número de asignaturas que tiene cada uno. Tenga en cuenta que pueden existir grados que no tienen asignaturas asociadas. Estos grados también tienen que aparecer en el listado. El resultado deberá estar ordenado de mayor a menor por el número de asignaturas.
    public async Task<IEnumerable<Object>>GetCantAsigNoAsosiadaPorGrado()
    {
        return await _context.Grados
                            .Select(p => new
                            {
                                Grado = p.Nombre,
                                AsignaturasAsociadas = p.Asignaturas.Where(a => p.Id == a.Id_Grado).Count() 
                            }).OrderByDescending(p => p.AsignaturasAsociadas).ToListAsync();
    }
    //Devuelve un listado con el nombre de todos los grados existentes en la base de datos y el número de asignaturas que tiene cada uno, de los grados que tengan más de `40` asignaturas asociadas.

    public async Task<IEnumerable<Object>>GetCantAsigPorcantidadEnGrado(int cantidadMinima)
    {
        return await _context.Grados
                            .Where(p => p.Asignaturas.Count() > cantidadMinima)
                            .Select(p => new
                            {
                                Grado = p.Nombre,
                                AsignaturasAsociadas = p.Asignaturas.Where(a => p.Id == a.Id_Grado).Count()
                            }).OrderByDescending(p => p.AsignaturasAsociadas).ToListAsync();
    }
    //23. Devuelve un listado que muestre el nombre de los grados y la suma del número total de créditos que hay para cada tipo de asignatura. El resultado debe tener tres columnas: nombre del grado, tipo de asignatura y la suma de los créditos de todas las asignaturas que hay de ese tipo. Ordene el resultado de mayor a menor por el número total de crédidos.

    public async Task<IEnumerable<Object>> GetGradosSumCreditos()
    {
        return await _context.Grados
            .Include(p => p.Asignaturas)
            .ThenInclude(p => p.TipoAsignatura)
            .Select(p => new
            {
                Grado = p.Nombre,
                TipoDeAsignatura = p.Asignaturas
                    .Select(asignatura => ((AsignaturaType)asignatura.Tipo).ToString())
                    .FirstOrDefault(),
                SumaCreditos = p.Asignaturas
                    .Where(asignatura => asignatura.Tipo == p.Asignaturas
                        .Select(a => a.Tipo)
                        .FirstOrDefault())
                    .Sum(asignatura => asignatura.Creditos)
            }).OrderByDescending(p => p.SumaCreditos)
            .ToListAsync();
    }
}

