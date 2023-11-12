using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class CursoEscolarRepository : GenericRepository<CursoEscolar>, ICursoEscolar
{
    private readonly UniversityContext _context;

    public CursoEscolarRepository(UniversityContext context) : base(context)
    {
        _context = context;
    }
    //24. Devuelve un listado que muestre cuántos alumnos se han matriculado de alguna asignatura en cada uno de los cursos escolares. El resultado deberá mostrar dos columnas, una columna con el año de inicio del curso escolar y otra con el número de alumnos matriculados.

    public async Task<Object> GetCantAlumnosMatrEnCurso()
    {
        return await _context.Matriculas
                    .Include(p => p.CursoEscolar)
                    .GroupBy(p => p.CursoEscolar,(key,group) => new
                    {
                        CursoEscolar = key.Anio_inicio,
                        AlumnosMatriculados = group.Select(p => p.Alumno).Count()
                    }).ToListAsync();
    }


}