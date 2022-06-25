using MobileStore.Authentication.Domain.Response;
using MobileStore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileStore.Authentication.Domain.Service
{
    public interface IAccountService
    {
        Task<UserResponse<User>> CreateUser(User user);
        Task<UserResponse<IEnumerable<User>>> GetAllUsers();
        Task<UserResponse<User>> GetUserByEmail(string email);
    }
}
