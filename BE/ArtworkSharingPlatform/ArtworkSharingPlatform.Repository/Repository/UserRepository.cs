using ArtworkSharingPlatform.Domain.Entities.Users;
using ArtworkSharingPlatform.Domain.Migrations;
using ArtworkSharingPlatform.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ArtworkSharingPlatform.Repository.Repository;

public class UserRepository : IUserRepository
{
    private readonly ArtworkSharingPlatformDbContext _dbContext;
    private readonly UserManager<User> _userManager;

    public UserRepository(ArtworkSharingPlatformDbContext dbContext, UserManager<User> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public User GetById(int id)
    {
        return _dbContext?.Users?.Where(user => user.Id == id)
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefault()!;
    }

    public IQueryable<User> GetAll()
    {
        return _dbContext.Users.Include(u => u.UserRoles);
    }

    public async Task CreateUserAdmin(User user)
    {
        try
        {
            var u = await _userManager.FindByEmailAsync(user.Email);
            if (u == null)
            {
                string password = "Pa$$w0rd";
                await _userManager.CreateAsync(user, password);
                /*await _userManager.SetUserNameAsync(user, user.Email);*/
            }
            else
            {
                throw new Exception("User is exist");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        
    }

    public async Task UpdateUserAdmin(User user)
    {
        try
        {
            var u = await _userManager.FindByEmailAsync(user.Email);
            if(u != null)
            {
                u.Name = user.Name;
                u.Email = user.Email;
                u.PhoneNumber = user.PhoneNumber;
                u.UserRoles = user.UserRoles;
                u.Status = user.Status;
                u.RemainingCredit = user.RemainingCredit;
                u.PackageId = user.PackageId;
                await _userManager.UpdateAsync(u);
            }
            else
            {
                throw new Exception("User is not exist");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeleteUserAdmin(User user)
    {
        try
        {
            var u = await _userManager.FindByEmailAsync(user.Email);
            if (u != null)
            {
                u.Status = 0;
                await _userManager.UpdateAsync(u);
            }
            else
            {
                throw new Exception("User is not exist");
            }
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task UpdateUserDetail(User user)
    {
        try
        {
            var u = await _userManager.FindByEmailAsync(user.Email);
            if (u != null)
            {
                u.Name = user.Name;
                u.Email = user.Email;
                u.PhoneNumber = user.PhoneNumber;
                await _userManager.UpdateAsync(u);
            }
            else
            {
                throw new Exception("User is not exist");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}