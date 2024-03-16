using ArtworkSharingPlatform.DataTransferLayer.Payload.Request;
using ArtworkSharingPlatform.Domain.Entities.Users;
using ArtworkSharingPlatform.Domain.Migrations;
using ArtworkSharingPlatform.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ArtworkSharingPlatformDbContext _dbContext;
        private readonly UserManager<User> _userManager;


        public UserRepository(ArtworkSharingPlatformDbContext context, UserManager<User> userManager)
        {
            _dbContext = context;
            _userManager = userManager;
        }

        public async Task<User> GetUserById(int id)
        {

            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
                return user;
            }
            catch (Exception ex)
            {


            }
            return null;
        }

        public User GetById(int id)
        {
            return _dbContext?.Users?.Where(user => user.Id == id)
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Include(u => u.UserImage)
                .FirstOrDefault()!;
        }

        public IQueryable<User> GetAll()
        {
            return _dbContext.Users.Include(u => u.UserRoles).Include(u => u.UserImage);
        }

        public async Task CreateUserAdmin(User user)
        {
            try
            {
                var u = await _userManager.FindByEmailAsync(user.Email);
                if (u == null)
                {
                    string password = "Pa$$w0rd";
                    if(await IsPhoneExistAsync(user.PhoneNumber))
                    {
                        throw new Exception("Phone is exist");
                    }
                    var result = await _userManager.CreateAsync(user, password);
                    if (!result.Succeeded)
                    {
                        throw new Exception("User creation failed");
                    }
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
                if (u != null)
                {
                    if (u.PhoneNumber != user.PhoneNumber && await IsPhoneExistAsync(user.PhoneNumber))
                    {
                        throw new Exception("Phone is exist");
                    }
                    u.Name = user.Name;
                    u.Email = user.Email;
                    u.PhoneNumber = user.PhoneNumber;
                    u.UserRoles = user.UserRoles;
                    u.UserImage = user.UserImage;
                    u.Description = user.Description;
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
            catch (Exception ex)
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
                    if (u.PhoneNumber != user.PhoneNumber && await IsPhoneExistAsync(user.PhoneNumber))
                    {
                        throw new Exception("Phone is exist");
                    }
                    u.Name = user.Name;
                    u.Email = user.Email;
                    u.PhoneNumber = user.PhoneNumber;
                    u.Description = user.Description;
                    u.TwitterLink = user.TwitterLink;
                    u.FacebookLink = user.FacebookLink;
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

        public async Task<bool> IsPhoneExistAsync(string phone)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phone);
            return user != null;
        }
	}
}