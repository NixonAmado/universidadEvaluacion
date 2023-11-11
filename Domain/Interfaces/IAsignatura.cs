using Domain.Entities;

namespace Domain.Interfaces;

    public interface IAsignatura : IGenericRepository<Asignatura>
    {
         Task<IEnumerable<Asignatura>> GetAsignaturasPorGrado(int cuatr,int curso,int grado);
         Task<IEnumerable<Asignatura>> GetAsignaturasOfertadasGrado();
         Task<IEnumerable<Asignatura>> GetAsignaturasSinProfesor();
    }
