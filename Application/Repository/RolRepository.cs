using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class RolRepository : GenericRepository<Rol>, IRol
{
    private readonly SkelettonContext _context;

    public RolRepository(SkelettonContext context) : base(context)
    {
       _context = context;
    }
}