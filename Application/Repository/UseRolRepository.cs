using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
public class UseRolRepository : GenericRepository<UserRol>, IUserRol
{
    private readonly UniversityContext _context;

    public UseRolRepository(UniversityContext context) : base(context)
    {
        _context = context;
    }
}