using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagementSystem.Data;
using CustomerManagementSystem.Domain;
using CustomerManagementSystem.Domain.Infastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomerManagementSystem.Controllers
{
    [Produces("application/json")] //burada yaratýlan tüm nesnelerin application json türünden nesneler olmasý gerektiðini bildirir
    [Route("api/Customers")]// bu sýnýfa api/customers ile ulaþýlabileceðini söylemekte bu sýnýfýn içindeki metodlara ulaþmak içinde bunun sonuna eklenecektir adresi.
    public class CustomersController : Controller
    {

        private ICustomersRepository _customersRepository;
        private ILogger _logger;
        public CustomersController(ICustomersRepository customersRepository, ILoggerFactory loggerFactory)
        {
            _customersRepository = customersRepository;
            _logger = loggerFactory.CreateLogger(nameof(CustomersController));

        }
      

        [HttpGet]
        [ProducesResponseType(typeof(List<Customer>), 200)]
        //context dönerse baþarýlý
        [ProducesResponseType(typeof(ApiResponse), 400)]
            //apiresponse dönerse baþarýsýz
        
        public async Task<IActionResult> Customers()
        {
            try
            {
                var customers = await _customersRepository.GetCustomerAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new ApiResponse { Status = false });

            }
        }
        //Api/Customers/{id} = api/customers/2
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Customer), 200)]
        //context dönerse baþarýlý
        [ProducesResponseType(typeof(ApiResponse), 400)]
        //apiresponse dönerse baþarýsýz

        public async Task<IActionResult> Customers(int id)
        {
            try
            {
                var customer = await _customersRepository.GetCustomerAsync(id);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new ApiResponse { Status = false });

            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Customer), 201)] //yeninesne yaratýldýðýnda 201 elde ederiz
        //context dönerse baþarýlý
        [ProducesResponseType(typeof(ApiResponse), 400)]
        //apiresponse dönerse baþarýsýz
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer) //bu customerý verinin cotentinden alcaksýn demek
        {
            if (!ModelState.IsValid)
            {
                //gelen veri customer sýnýfýnýn alanlarýna uyumlu deðilsee
                return BadRequest(new ApiResponse {Status = false, ModalState = ModelState});
            }
           
                try
                {
                    var newcustomer = await _customersRepository.InsertCustomerAsync(customer);
                    if (newcustomer==null)
                    {
                        return BadRequest(new ApiResponse {Status = false});
                    }
                    return CreatedAtRoute("", new {id = newcustomer.Id},new ApiResponse{Status = true,Customer = newcustomer});//yeni yaratýlan nesne için link oluþturduk
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    return BadRequest(new ApiResponse {Status = false});
                }
            
        }
    }
}