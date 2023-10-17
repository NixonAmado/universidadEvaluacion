using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
public class UseRolRepository : GenericRepository<UserRol>, IUserRol
{
    private readonly SkelettonContext _context;

    public UseRolRepository(SkelettonContext context) : base(context)
    {
        _context = context;
    }
}