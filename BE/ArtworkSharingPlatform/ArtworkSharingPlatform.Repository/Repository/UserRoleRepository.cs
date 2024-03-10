using ArtworkSharingPlatform.Domain.Entities.Users;
using ArtworkSharingPlatform.Domain.Migrations;
using ArtworkSharingPlatform.Repository.Interfaces;

namespace ArtworkSharingPlatform.Repository.Repository;

public class UserRoleRepository:IUserRoleRepository
{
    private readonly ArtworkSharingPlatformDbContext _dbContext;

    public UserRoleRepository(ArtworkSharingPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public List<string> GetRolesByUserId(int id)
    {
        var roleNames = (from anu in _dbContext.Users
            join anur in _dbContext.UserRoles on anu.Id equals anur.UserId into userRoles
            from anur in userRoles.DefaultIfEmpty()
            join anr in _dbContext.Roles on anur.RoleId equals anr.Id into roles
            from anr in roles.DefaultIfEmpty()
            where anu.Id == id
            select 
                anr.Name
            ).ToList();
        return roleNames;
    }
}