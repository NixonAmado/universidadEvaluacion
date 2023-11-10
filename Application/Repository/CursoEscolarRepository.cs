using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
public class CursoEscolarRepository : GenericRepository<CursoEscolar>, ICursoEscolar
{
    private readonly UniversityContext _context;

    public CursoEscolarRepository(UniversityContext context) : base(context)
    {
        _context = context;
    }
}