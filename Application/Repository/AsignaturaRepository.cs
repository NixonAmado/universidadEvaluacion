using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
public class AsignaturaRepository : GenericRepository<Asignatura>, IAsignatura
{
    private readonly UniversityContext _context;

    public AsignaturaRepository(UniversityContext context) : base(context)
    {
        _context = context;
    }
}