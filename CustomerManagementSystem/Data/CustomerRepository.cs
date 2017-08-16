using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagementSystem.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CustomerManagementSystem.Data
{
    public class CustomerRepository:ICustomersRepository
    {

        private CustomerContext _customerContext;
        private ILogger _logger;

        public CustomerRepository(CustomerContext customerContext,ILoggerFactory _loggerFactory)
        {
            _customerContext = customerContext;
            _logger=_loggerFactory.CreateLogger("CustomerRepository");
        }

        public async Task<List<Customer>> GetCustomerAsync()
        {
            return await _customerContext.Customers.OrderBy(x => x.LastName).Include(s=>s.State).Include(o=>o.Orders).ToListAsync();
            //İNCLUDE FONKSİYONU SEÇİLEN CUSTOMER İLE İLGİLİ TÜM STATE VE ORDER BİLGİLERİNİDE ALMAK İÇİN YAZILIR
        }

        public async Task<Customer> GetCustomerAsync(int Id)
        {
            return await _customerContext.Customers.Include(s => s.State).Include(o => o.Orders).SingleOrDefaultAsync(x=>x.Id==Id);
            //İNCLUDE FONKSİYONU SEÇİLEN CUSTOMER İLE İLGİLİ TÜM STATE VE ORDER BİLGİLERİNİDE ALMAK İÇİN YAZILIR
        }

        public async Task<Customer> InsertCustomerAsync(Customer customer)
        {
            _customerContext.Add(customer);
            try
            {
                await _customerContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                _logger.LogError($"Error in {nameof(InsertCustomerAsync)} :"+e.Message);
            }
            return customer;
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            _customerContext.Customers.Attach(customer);
            //customer contextin iinde olmayabilir önce nu attach ediyoruz sonra da customerın durumunuda contexte değiştirildidiye kaydediyoruz
            _customerContext.Entry(customer).State = EntityState.Modified;
            try
            {
                return (await _customerContext.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception e)
            {
               _logger.LogError($"Error in {nameof(UpdateCustomerAsync)} :"+e.Message);
            }

            return false;
        }

        public async Task<bool> DeleteCustomerAsync(int Id)
        {
            var customer = _customerContext.Customers.Include(o => o.Orders).SingleOrDefaultAsync(x => x.Id == Id);
            _customerContext.Remove(customer);
            try
            {
                return (await _customerContext.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception e)
            {
               _logger.LogError($"Error in {nameof(DeleteCustomerAsync)} :"+e.Message);
            }
            return false;
        }
    }
}
