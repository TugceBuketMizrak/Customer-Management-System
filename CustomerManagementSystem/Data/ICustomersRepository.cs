using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagementSystem.Domain;

namespace CustomerManagementSystem.Data
{
    public interface ICustomersRepository
    {
        Task<List<Customer>> GetCustomerAsync();
        Task<Customer> GetCustomerAsync(int Id);
        Task<Customer> InsertCustomerAsync(Customer customer);
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(int Id);


    }
}
