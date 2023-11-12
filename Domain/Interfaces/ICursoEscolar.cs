using Domain.Entities;

namespace Domain.Interfaces;

    public interface ICursoEscolar : IGenericRepository<CursoEscolar>
    {
        Task<Object> GetCantAlumnosMatrEnCurso();
    }
