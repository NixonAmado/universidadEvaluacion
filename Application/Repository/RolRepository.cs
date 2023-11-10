using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class RolRepository : GenericRepository<Rol>, IRol
{
    private readonly UniversityContext _context;

    public RolRepository(UniversityContext context) : base(context)
    {
       _context = context;
    }
}