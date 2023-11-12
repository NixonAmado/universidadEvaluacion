using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class ProfesorRepository : GenericRepository<Profesor>, IProfesor
{
    private readonly UniversityContext _context;

    public ProfesorRepository(UniversityContext context) : base(context)
    {
        _context = context;
    }
    //4. Devuelve el listado de `profesores` que **no** han dado de alta su número de teléfono en la base de datos y además su nif termina en `K`.
    public async Task<IEnumerable<Persona>> GetProfesoresConNumero(string letter)
    {
        return await _context.Personas
                            .Where(p => p.Tipo == Persona.Type.profesor &&
                             !string.IsNullOrEmpty(p.Telefono) && 
                             p.Nif.EndsWith(letter))
                            .ToListAsync();
    }
    //8. Devuelve un listado de los `profesores` junto con el nombre del `departamento` al que están vinculados. El listado debe devolver cuatro columnas, `primer apellido, segundo apellido, nombre y nombre del departamento.` El resultado estará ordenado alfabéticamente de menor a mayor por los `apellidos y el nombre.`

    public async Task<IEnumerable<Profesor>> GetProfesoresDepartamento()
    {
        return await _context.Profesores
                            .Include(p => p.Persona)
                            .Include(p => p.Departamento)
                            .OrderBy(p => p.Persona.Apellido1)
                            .ThenBy(p => p.Persona.Apellido2)
                            .ThenBy(p => p.Persona.Nombre)
                            .ToListAsync();
    }

//12. Devuelve un listado con los nombres de **todos** los profesores y los departamentos que tienen vinculados. El listado también debe mostrar aquellos profesores que no tienen ningún departamento asociado. El listado debe devolver cuatro columnas, nombre del departamento, primer apellido, segundo apellido y nombre del profesor. El resultado estará ordenado alfabéticamente de menor a mayor por el nombre del departamento, apellidos y el nombre.
    public async Task<IEnumerable<Profesor>> GetAllProfesores()
    {
        return await _context.Profesores
                            .Include(p => p.Departamento)
                            .Include(p => p.Persona)
                            .OrderBy(p => p.Departamento)
                            .ThenBy(p => p.Persona.Apellido1)
                            .ThenBy(p => p.Persona.Apellido2)
                            .ThenBy(p => p.Persona.Nombre)
                            .ToListAsync();
    }
    //13. Devuelve un listado con los profesores que no están asociados a un departamento.Devuelve un listado con los departamentos que no tienen profesores asociados.

    public async Task<(IEnumerable<Profesor> profesores ,IEnumerable<Departamento> departamentos)> GetProfesoresYDepartamentos()
    {
        var profesores =  await _context.Profesores
                            .Include(p => p.Persona)
                            .Include(p => p.Departamento)
                            .Where(p => p.Departamento == null)
                            .ToListAsync();
        
        var departamentos =  await _context.Departamentos
                            .Where(p => p.Profesores.Count() == 0)
                            .ToListAsync();   

        return (profesores, departamentos);}

    //14. Devuelve un listado con los profesores que no imparten ninguna asignatura.
    public async Task<IEnumerable<Profesor>> GetProfesoresNoAsignatura()
    {
        return await _context.Profesores
                            .Where(p => !p.Asignaturas.Any())
                            .ToListAsync();
    }
    

    //25. Devuelve un listado con el número de asignaturas que imparte cada profesor. El listado debe tener en cuenta aquellos profesores que no imparten ninguna asignatura. El resultado mostrará cinco columnas: id, nombre, primer apellido, segundo apellido y número de asignaturas. El resultado estará ordenado de mayor a menor por el número de asignaturas.

    public async Task<IEnumerable<Object>> GetProfesorConAsignaturas()
    {
        return await _context.Profesores
                            .Select(p => new 
                            {
                                id = p.Id_profesor,
                                primerApellido = p.Persona.Apellido1,
                                SegundoApellido = p.Persona.Apellido2,
                                numeroAsignaturas = p.Asignaturas.Count()
                            }).OrderByDescending(p => p.numeroAsignaturas).ToListAsync();
    }
    
    //27. Devuelve un listado con los profesores que no están asociados a un departamento.
    public async Task<IEnumerable<Profesor>> GetAllProfesoresNoVinculados()
    {
        return await _context.Profesores
                            .Where(p => p.Departamento == null).ToListAsync();
    }
    //29. Devuelve un listado con los profesores que tienen un departamento asociado y que no imparten ninguna asignatura.
    public async Task<IEnumerable<Profesor>> GetProfesoresDepartamentoSinAsig()
    {
        return await _context.Profesores
                            .Include(p => p.Departamento)
                            .Include(p =>p.Persona)
                            .Where(p => p.Id_departamento == p.Departamento.Id && !p.Asignaturas.Any()).ToListAsync();
    }
    


}
