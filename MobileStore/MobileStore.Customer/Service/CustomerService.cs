using Microsoft.EntityFrameworkCore;
using MobileStore.Customer.Domain.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MobileStore.Domain.Entities;

namespace MobileStore.Customer.Domain.Service
{
    public class CustomerService:ICustomerService
    {
        private readonly DataContext _db;

        public CustomerService(DataContext db) => _db = db;

        public async Task<CustomerResponse<Person>> CreateCustomer(Person person)
        {
            var response = new CustomerResponse<Person>();

            try
            {
                await _db.AddAsync(person);
                await _db.SaveChangesAsync();
                response.Description = "Заказчик успешно добавлен";
                response.Data = person;
            }
            catch (Exception ex)
            {
                return new CustomerResponse<Person>() { Description = $"Ошибка {ex.Message}" };
            }
            return response;
        }

        public async Task<CustomerResponse<IEnumerable<Person>>> GetAllCustomers()
        {
            var response = new CustomerResponse<IEnumerable<Person>>();

            try
            {
                var personList = await _db.Customers.ToListAsync();

                response.Data = personList;
                response.Description = "Успешный запрос";
            }
            catch (Exception ex)
            {
                return new CustomerResponse<IEnumerable<Person>> { Description = $"Ошибка {ex.Message}." };
            }
            return response;
        }

        public async Task<CustomerResponse<Person>> GetCustomerById(int id)
        {
            var response = new CustomerResponse<Person>();

            try
            {
                var person = await _db.Customers.FirstOrDefaultAsync(x => x.Id == id);

                if (person == null)
                    response.Description = "Пользователь не найден";
                else
                {
                    response.Data = person;
                    response.Description = "Успешный запрос";
                }
            }
            catch (Exception ex)
            {
                return new CustomerResponse<Person>() { Description = $"Ошибка {ex.Message}" };
            }

            return response;
        }
    }
}

