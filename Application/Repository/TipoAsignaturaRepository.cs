using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
public class TipoAsignaturaRepository : GenericRepository<TipoAsignatura>, ITipoAsignatura
{
    private readonly UniversityContext _context;

    public TipoAsignaturaRepository(UniversityContext context) : base(context)
    {
        _context = context;
    }
}