using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
public class PersonaRepository : GenericRepository<Persona>, IPersona
{
    private readonly UniversityContext _context;

    public PersonaRepository(UniversityContext context) : base(context)
    {
        _context = context;
    }
}