namespace Domain.Interfaces;

public interface IUnitOfWork
{
    IRol Roles { get; }
    IUser Users { get; }
    IUserRol UserRoles { get; }
    Task<int> SaveAsync();
}