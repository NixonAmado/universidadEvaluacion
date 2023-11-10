using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
public class DepartamentoRepository : GenericRepository<Departamento>, IDepartamento
{
    private readonly UniversityContext _context;

    public DepartamentoRepository(UniversityContext context) : base(context)
    {
        _context = context;
    }
}