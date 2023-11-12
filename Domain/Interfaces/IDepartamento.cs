using Domain.Entities;

namespace Domain.Interfaces;

    public interface IDepartamento : IGenericRepository<Departamento>
    {
        Task<IEnumerable<Departamento>> GetDepartamentosPorProfesores();
        Task<IEnumerable<Object>> GetCantProfesoresEnDepartamento();
        Task<IEnumerable<Object>> GetDepartamentoTieneAsignatura();
        Task<IEnumerable<Object>> GetCantProfNoAsosiadosDepart();
        Task<IEnumerable<Departamento>> GetDepartamentoSinProfesores();
        Task<IEnumerable<Departamento>> GetDepartamentoSinAsigCS();
    }
