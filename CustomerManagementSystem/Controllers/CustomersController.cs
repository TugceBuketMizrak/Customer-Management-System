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
    [Produces("application/json")] //burada yarat�lan t�m nesnelerin application json t�r�nden nesneler olmas� gerekti�ini bildirir
    [Route("api/Customers")]// bu s�n�fa api/customers ile ula��labilece�ini s�ylemekte bu s�n�f�n i�indeki metodlara ula�mak i�inde bunun sonuna eklenecektir adresi.
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
        //context d�nerse ba�ar�l�
        [ProducesResponseType(typeof(ApiResponse), 400)]
            //apiresponse d�nerse ba�ar�s�z
        
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
        //context d�nerse ba�ar�l�
        [ProducesResponseType(typeof(ApiResponse), 400)]
        //apiresponse d�nerse ba�ar�s�z

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
        [ProducesResponseType(typeof(Customer), 201)] //yeninesne yarat�ld���nda 201 elde ederiz
        //context d�nerse ba�ar�l�
        [ProducesResponseType(typeof(ApiResponse), 400)]
        //apiresponse d�nerse ba�ar�s�z
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer) //bu customer� verinin cotentinden alcaks�n demek
        {
            if (!ModelState.IsValid)
            {
                //gelen veri customer s�n�f�n�n alanlar�na uyumlu de�ilsee
                return BadRequest(new ApiResponse {Status = false, ModalState = ModelState});
            }
           
                try
                {
                    var newcustomer = await _customersRepository.InsertCustomerAsync(customer);
                    if (newcustomer==null)
                    {
                        return BadRequest(new ApiResponse {Status = false});
                    }
                    return CreatedAtRoute("", new {id = newcustomer.Id},new ApiResponse{Status = true,Customer = newcustomer});//yeni yarat�lan nesne i�in link olu�turduk
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    return BadRequest(new ApiResponse {Status = false});
                }
            
        }
    }
}