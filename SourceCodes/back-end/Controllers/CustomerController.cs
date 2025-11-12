using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project;
using Project.Services;
using System.ComponentModel.DataAnnotations;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IdGenerator _idGenerator;

        public CustomersController(ICustomerService customerService, IdGenerator idGenerator)
        {
            _customerService = customerService;
            _idGenerator = idGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> Get()
        {
            return await _customerService.GetAllCustomersAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if(customer == null)
            {
                return NotFound();
            }
            return customer;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            try
            {
                await _customerService.AddCustomerAsync(customer.ad, customer.soyad, customer.adres, customer.telefon, customer.eposta);
                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Customer customer)
        {
            if(id != customer.customer_id)
            {
                return BadRequest(new { error = "Gönderilen müşteri kimliği URL'deki kimlikle eşleşmiyor." });
            }
            try
            {
                await _customerService.UpdateCustomerAsync(customer);
                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }
    }
}



/*


using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project;
using Project.Services;
using System.ComponentModel.DataAnnotations;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IdGenerator _idGenerator; // IdGenerator özelliği

        public CustomersController(ICustomerService customerService, IdGenerator idGenerator) // IdGenerator enjekte ediliyor
        {
            _customerService = customerService;
            _idGenerator = idGenerator; // Enjekte edilen IdGenerator örneği atama
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> Get()
        {
            return await _customerService.GetAllCustomersAsync();
        }

        [HttpGet("{id}")] 
        public async Task<ActionResult<Customer>> Get(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if(customer == null)
            {
                return NotFound();
            }
            return customer;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            try
            {
                // _idGenerator kullanılarak Id oluşturuluyor
                await _customerService.AddCustomerAsync(customer.ad, customer.soyad, customer.adres, customer.telefon, customer.eposta, _idGenerator);
                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Customer customer)
        {
            if(id != customer.customer_id)
            {
                return BadRequest(new { error = "Gönderilen müşteri kimliği URL'deki kimlikle eşleşmiyor." });
            }
            try
            {
                await _customerService.UpdateCustomerAsync(customer);
                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }
    }
}



*/