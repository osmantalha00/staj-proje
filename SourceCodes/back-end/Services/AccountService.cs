using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MongoDB.Driver;
using Project;

namespace Project.Services
{
    public interface IAccountService
    {
        Task<List<Account>> GetAllAccountsAsync();
        Task<Account> GetAccountByIdAsync(int id);
        Task AddAccountAsync(Account account, IdGenerator idGenerator);
        Task UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(int id);
    }

    public class AccountService : IAccountService
    {
        private readonly IMongoCollection<Account> _accounts;

        public AccountService(Connect connection)
        {
            _accounts = connection.accountsCollection;
        }

        public async Task<List<Account>> GetAllAccountsAsync()
        {
            return await _accounts.Find(account => true).ToListAsync();
        }

        public async Task<Account> GetAccountByIdAsync(int id)
        {
            return await _accounts.Find<Account>(account => account.account_id == id).FirstOrDefaultAsync();
        }

        public async Task AddAccountAsync(Account account, IdGenerator idGenerator)
        {
            account.account_id = idGenerator.GenerateAccountId();
            account.acilis_tarihi = DateTime.Now;

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(account, new ValidationContext(account), validationResults);

            if (!isValid)
            {
                throw new ValidationException("Hesap verileri hatalı: " + string.Join(", ", validationResults.Select(vr => vr.ErrorMessage)));
            }

            await _accounts.InsertOneAsync(account);
        }

        public async Task UpdateAccountAsync(Account accountIn)
        {
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(accountIn, new ValidationContext(accountIn), validationResults);

            if (!isValid)
            {
                throw new ValidationException("Hesap verileri hatalı: " + string.Join(", ", validationResults.Select(vr => vr.ErrorMessage)));
            }

            await _accounts.ReplaceOneAsync(account => account.account_id == accountIn.account_id, accountIn);
        }

        public async Task DeleteAccountAsync(int id)
        {
            await _accounts.DeleteOneAsync(account => account.account_id == id);
        }
    }
}
