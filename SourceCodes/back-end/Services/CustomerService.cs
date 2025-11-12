using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Project;
using Project.Services;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Project.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task AddCustomerAsync(string name, string surname, string address, string phone, string email);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int id);
    }

    public class CustomerService : ICustomerService
    {
        private readonly IMongoCollection<Customer> _customers;
        private readonly IdGenerator _idGenerator; 

        public CustomerService(Connect connection, IdGenerator idGenerator)
        {
            _customers = connection.customersCollection;
            _idGenerator = idGenerator; 
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _customers.Find(customer => true).ToListAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _customers.Find<Customer>(customer => customer.customer_id == id).FirstOrDefaultAsync();
        }

        public async Task AddCustomerAsync(string name, string surname, string address, string phone, string email)
        {
            var customer = new Customer
            {
                customer_id = _idGenerator.GenerateCustomerId(), // Enjekte edilen IdGenerator kullanılıyor
                ad = name,
                soyad = surname,
                adres = address,
                telefon = phone,
                eposta = email
            };

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(customer, new ValidationContext(customer), validationResults);

            if (!isValid)
            {
                throw new ValidationException("Müşteri verileri hatalı: " + string.Join(", ", validationResults.Select(vr => vr.ErrorMessage)));
            }

            await _customers.InsertOneAsync(customer);
        }

        public async Task UpdateCustomerAsync(Customer customerIn)
        {
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(customerIn, new ValidationContext(customerIn), validationResults);

            if (!isValid)
            {
                throw new ValidationException("Müşteri verileri hatalı: " + string.Join(", ", validationResults.Select(vr => vr.ErrorMessage)));
            }

            await _customers.ReplaceOneAsync(customer => customer.customer_id == customerIn.customer_id, customerIn);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            await _customers.DeleteOneAsync(customer => customer.customer_id == id);
        }
    }
}





/*
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Project;
using Project.Services;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Project.Services{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task AddCustomerAsync(string name, string surname, string address, string phone, string email, IdGenerator idGenerator);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int id);
    }

    public class CustomerService : ICustomerService
    {
        private readonly IMongoCollection<Customer> _customers;

        public CustomerService(Connect connection)
        {
            _customers = connection.customersCollection;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _customers.Find(customer => true).ToListAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _customers.Find<Customer>(customer => customer.customer_id == id).FirstOrDefaultAsync();
        }

        public async Task AddCustomerAsync(string name, string surname, string address, string phone, string email, IdGenerator idGenerator)
        {
            var customer = new Customer
            {
                customer_id = idGenerator.GenerateCustomerId(),
                ad = name,
                soyad = surname,
                adres = address,
                telefon = phone,
                eposta = email
            };

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(customer, new ValidationContext(customer), validationResults);

            if (!isValid)
            {
                throw new ValidationException("Müşteri verileri hatalı: " + string.Join(", ", validationResults.Select(vr => vr.ErrorMessage)));
            }

            await _customers.InsertOneAsync(customer);
        }

        public async Task UpdateCustomerAsync(Customer customerIn)
        {
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(customerIn, new ValidationContext(customerIn), validationResults);

            if (!isValid)
            {
                throw new ValidationException("Müşteri verileri hatalı: " + string.Join(", ", validationResults.Select(vr => vr.ErrorMessage)));
            }

            await _customers.ReplaceOneAsync(customer => customer.customer_id == customerIn.customer_id, customerIn);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            await _customers.DeleteOneAsync(customer => customer.customer_id == id);
        }
    }
}













*/