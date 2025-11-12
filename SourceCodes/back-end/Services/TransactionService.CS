using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Project;
using Project.Services;
using System;

namespace Project.Services
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetAllTransactionsAsync();
        Task<Transaction> GetTransactionByIdAsync(int id);
        Task AddTransactionAsync(Transaction transaction, IdGenerator idGenerator);
        Task UpdateTransactionAsync(Transaction transaction);
        Task DeleteTransactionAsync(int id);
    }

    public class TransactionService : ITransactionService
    {
        private readonly IMongoCollection<Transaction> _transactions;

        public TransactionService(Connect connection)
        {
            _transactions = connection.transactionsCollection;
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await _transactions.Find(transaction => true).ToListAsync();
        }

        public async Task<Transaction> GetTransactionByIdAsync(int id)
        {
            return await _transactions.Find<Transaction>(transaction => transaction.transaction_id == id).FirstOrDefaultAsync();
        }

        public async Task AddTransactionAsync(Transaction transaction, IdGenerator idGenerator)
        {
            ValidateTransaction(transaction);
            transaction.transaction_id = idGenerator.GenerateTransactionId(); // ID oluşturuluyor
            transaction.tarih = DateTime.Now;
            await _transactions.InsertOneAsync(transaction);
        }

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            ValidateTransaction(transaction);
            await _transactions.ReplaceOneAsync(trans => trans.transaction_id == transaction.transaction_id, transaction);
        }

        public async Task DeleteTransactionAsync(int id)
        {
            await _transactions.DeleteOneAsync(transaction => transaction.transaction_id == id);
        }

        private void ValidateTransaction(Transaction transaction)
        {
            var context = new ValidationContext(transaction, null, null);
            Validator.ValidateObject(transaction, context, validateAllProperties: true);
        }
    }
}





/* 



using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Project;
using Project.Services;
using System;

namespace Project.Services
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetAllTransactionsAsync();
        Task<Transaction> GetTransactionByIdAsync(int id);
        Task AddTransactionAsync(Transaction transaction, IdGenerator idGenerator);
        Task UpdateTransactionAsync(Transaction transaction);
        Task DeleteTransactionAsync(int id);
    }

    public class TransactionService : ITransactionService
    {
        private readonly IMongoCollection<Transaction> _transactions;

        public TransactionService(Connect connection)
        {
            _transactions = connection.transactionsCollection;
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await _transactions.Find(transaction => true).ToListAsync();
        }

        public async Task<Transaction> GetTransactionByIdAsync(int id)
        {
            return await _transactions.Find<Transaction>(transaction => transaction.transaction_id == id).FirstOrDefaultAsync();
        }

        public async Task AddTransactionAsync(Transaction transaction, IdGenerator idGenerator)
        {
            ValidateTransaction(transaction);
            transaction.transaction_id = idGenerator.GenerateTransactionId(); // ID oluşturuluyor
            transaction.tarih = DateTime.Now;
            await _transactions.InsertOneAsync(transaction);
        }

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            ValidateTransaction(transaction);
            await _transactions.ReplaceOneAsync(trans => trans.transaction_id == transaction.transaction_id, transaction);
        }

        public async Task DeleteTransactionAsync(int id)
        {
            await _transactions.DeleteOneAsync(transaction => transaction.transaction_id == id);
        }

        private void ValidateTransaction(Transaction transaction)
        {
            var context = new ValidationContext(transaction, null, null);
            Validator.ValidateObject(transaction, context, validateAllProperties: true);
        }
    }
}





*/