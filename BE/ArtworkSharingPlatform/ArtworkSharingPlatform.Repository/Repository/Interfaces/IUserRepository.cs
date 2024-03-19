using ArtworkSharingPlatform.DataTransferLayer.Payload.Request;
using ArtworkSharingPlatform.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int id);
        User GetByEmail(string email);
        User GetById(int id);
        IQueryable<User> GetAll();
        Task CreateUserAdmin(User user);
        Task UpdateUserAdmin(User user);
        Task DeleteUserAdmin(User user);
        Task DeleteUserAdminByEmail(string email);
        Task UpdateUserDetail(User user);

    }
}