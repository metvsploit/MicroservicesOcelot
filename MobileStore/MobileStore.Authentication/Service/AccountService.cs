using Microsoft.EntityFrameworkCore;
using MobileStore.Authentication.Domain.Data;
using MobileStore.Authentication.Domain.Response;
using MobileStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileStore.Authentication.Domain.Service
{
    public class AccountService : IAccountService
    {
        private readonly DataContext _db;

        public AccountService(DataContext db) => _db = db;

        public async Task<UserResponse<User>> CreateUser(User user)
        {
            var response = new UserResponse<User>();

            try
            {
                await _db.AddAsync(user);
                await _db.SaveChangesAsync();
                response.Description = "Пользователь успешно добавлен";
                response.Data = user;
            }
            catch (Exception ex)
            {
                return new UserResponse<User>() { Description = $"Ошибка {ex.Message}" };
            }
            return response;
        }

        public async Task<UserResponse<IEnumerable<User>>> GetAllUsers()
        {
            var response = new UserResponse<IEnumerable<User>>();

            try
            {
                var usersList = await _db.Users.ToListAsync();

                response.Data = usersList;
                response.Description = "Успешный запрос";
            }
            catch (Exception ex)
            {
                return new UserResponse<IEnumerable<User>> { Description = $"Ошибка {ex.Message}." };
            }
            return response;
        }

        public async Task<UserResponse<User>> GetUserByEmail(string email)
        {
            var response = new UserResponse<User>();

            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
                if(user == null)
                    response.Description = "Пользователь не найден";
                else
                {
                    response.Data = user;
                    response.Description = "Пользователь найден";
                }
                return response;
            }
            catch(Exception ex)
            {
                return new UserResponse<User> { Description = $"Ошибка {ex.Message}." };
            }
        }
    }
}
