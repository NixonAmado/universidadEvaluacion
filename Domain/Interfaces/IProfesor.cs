using Domain.Entities;

namespace Domain.Interfaces;

    public interface IProfesor : IGenericRepository<Profesor>
    {
        Task<IEnumerable<Persona>> GetProfesoresConNumero(string letter);
        Task<IEnumerable<Profesor>> GetProfesoresDepartamento();
        Task<IEnumerable<Profesor>> GetAllProfesores();
        Task<IEnumerable<Profesor>> GetProfesoresNoAsignatura();
        Task<IEnumerable<Object>> GetProfesorConAsignaturas();
        Task<IEnumerable<Profesor>> GetAllProfesoresNoVinculados();
        Task<IEnumerable<Profesor>> GetProfesoresDepartamentoSinAsig();
    }
