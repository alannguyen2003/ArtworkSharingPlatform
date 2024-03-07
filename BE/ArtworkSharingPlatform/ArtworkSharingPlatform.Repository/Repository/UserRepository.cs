using ArtworkSharingPlatform.Domain.Entities.Users;
using ArtworkSharingPlatform.Domain.Migrations;
using ArtworkSharingPlatform.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ArtworkSharingPlatformDbContext _context;

        public UserRepository(ArtworkSharingPlatformDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserById(int id)
        {
            
            try
            {
               var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                return user;
            } catch (Exception ex)
            {

            }
            return null;
        }
    }
}
