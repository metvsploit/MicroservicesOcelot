using MobileStore.Customer.Domain.Response;
using System.Collections.Generic;
using System.Threading.Tasks;
using MobileStore.Domain.Entities;

namespace MobileStore.Customer.Domain.Service
{
    public interface ICustomerService
    {
        Task<CustomerResponse<Person>> CreateCustomer(Person person);
        Task<CustomerResponse<Person>> GetCustomerById(int id);
        Task<CustomerResponse<IEnumerable<Person>>> GetAllCustomers();
    }
}
