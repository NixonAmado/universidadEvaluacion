using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
public class GradoRepository : GenericRepository<Grado>, IGrado
{
    private readonly UniversityContext _context;

    public GradoRepository(UniversityContext context) : base(context)
    {
        _context = context;
    }
}