using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
public class ProfesorRepository : GenericRepository<Profesor>, IProfesor
{
    private readonly UniversityContext _context;

    public ProfesorRepository(UniversityContext context) : base(context)
    {
        _context = context;
    }
}