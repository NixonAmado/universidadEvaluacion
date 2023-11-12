using Domain.Entities;

namespace Domain.Interfaces;

    public interface IGrado : IGenericRepository<Grado>
    {
        Task<IEnumerable<Object>>GetCantAsigNoAsosiadaPorGrado();
        Task<IEnumerable<Object>>GetCantAsigPorcantidadEnGrado(int cantidadMinima);
        Task<IEnumerable<Object>>GetGradosSumCreditos();
    }
    
