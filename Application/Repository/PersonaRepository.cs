using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class PersonaRepository : GenericRepository<Persona>, IPersona
{
    private readonly UniversityContext _context;

    public PersonaRepository(UniversityContext context) : base(context)
    {
        _context = context;
    }

    // ---------------------------------ALUMNOS------------------------------
    //1. Devuelve un listado con el primer apellido, segundo apellido y el nombre de todos los alumnos. El listado deberá estar ordenado alfabéticamente de menor a mayor por el primer apellido, segundo apellido y nombre. --OK

    public async Task<IEnumerable<Persona>> GetAlumnosOrdendos()
    {
        return await _context.Personas
                    .Where(p => p.Tipo == Persona.Type.alumno )
                    .OrderBy(p => p.Apellido1)
                    .ThenBy(p => p.Apellido2)
                    .ThenBy(p => p.Nombre)
                    .ToListAsync();
    }
//2. Averigua el nombre y los dos apellidos de los alumnos que **no** han dado de alta su número de teléfono en la base de datos. --OK
    public async Task<IEnumerable<Persona>> GetAlumnosConNumero()
    {
        return await _context.Personas
                    .Where(p => p.Tipo == Persona.Type.alumno && !string.IsNullOrEmpty(p.Telefono) )
                    .ToListAsync();
    }
//3. Devuelve el listado de los alumnos que nacieron en `X`. -- OK
    public async Task<IEnumerable<Persona>> GetAlumnosNacidosEnX(int year)
    {
        return await _context.Personas
                    .Where(p => p.Tipo == Persona.Type.alumno && p.Fecha_nacimiento.Year == year)
                    .ToListAsync();
    }

//6. Devuelve un listado con los datos de todas las **alumnas** que se han matriculado alguna vez en el `Grado en Ingeniería Informática (Plan 2015)`.
    public async Task<IEnumerable<Persona>> GetAlumnasMatriculadas()
    {
        return await _context.Personas
                    .Where(p => p.Tipo == Persona.Type.alumno && 
                    p.Sexo == Persona.Genero.M &&
                    p.Matriculas.Any(m => m.Asignatura.Grado.Nombre == "Grado en Ingeniería Informática (Plan 2015)"))
                    .ToListAsync();
    }

//9. Devuelve un listado con el nombre de las asignaturas, año de inicio y año de fin del curso escolar del alumno con nif `26902806M`.
    public async Task<IEnumerable<AlumnoAsignatura>> GetAsignaturasPorAlumno(string nif)
    {       
        return await _context.Personas
                            
                            .Where(p => p.Tipo == Persona.Type.alumno && p.Nif.ToUpper() == nif.ToUpper())
                            .Select(p => new AlumnoAsignatura
                            {
                                Nombre = p.Nombre,
                                Asignaturas = p.Matriculas.Where(p => p.Id_alumno == p.Id_alumno).Select(p => p.Asignatura).ToList(),
                                Anio_fin = p.Matriculas.GroupBy(p => p.Id_alumno).Select(group => group.FirstOrDefault().CursoEscolar.Anio_fin).FirstOrDefault()

                            })
                            .ToListAsync();
    }
//11. Devuelve un listado con todos los alumnos que se han matriculado en alguna asignatura durante el curso escolar 2018/2019.
    public async Task<IEnumerable<Persona>> GetAlumnosMatriculados()
    {       
        return await _context.Personas
                            .Where(p => p.Matriculas.Any(m => m.Id_alumno == p.Id) &&
                             p.Matriculas.Any(p => 
                             p.CursoEscolar.Anio_inicio == 2018 &&
                             p.CursoEscolar.Anio_fin == 2019 )).Distinct()
                             .ToListAsync();

    }
}