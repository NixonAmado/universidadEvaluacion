using Domain.Entities;

namespace Domain.Interfaces;

    public interface IPersona : IGenericRepository<Persona>
    {
        Task<IEnumerable<Persona>> GetAlumnosOrdendos();
        Task<IEnumerable<Persona>> GetAlumnosConNumero();
        Task<IEnumerable<Persona>> GetAlumnosNacidosEnX(int year);
        Task<IEnumerable<Persona>> GetAlumnasMatriculadas();
        Task<IEnumerable<AlumnoAsignatura>> GetAsignaturasPorAlumno(string nif);
        Task<IEnumerable<Persona>> GetAlumnosMatriculados();
        
        
        
    }
