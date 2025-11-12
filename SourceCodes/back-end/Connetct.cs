using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Project
{
    public class Connect
    {
        private const string connectionString = "mongodb://localhost:27017"; // MongoDB bağlantı dizesini 
        private const string databaseName = "Bank5"; // MongoDB veritabanı adını 

        private readonly IMongoClient mongoClient;
        private readonly IMongoDatabase database;

        public List<Customer> Customers {get;}
        public List<Account> Accounts {get;}
        public List<Transaction> Transactions {get;}


        public IMongoCollection<Customer> customersCollection;
        public IMongoCollection<Account> accountsCollection;
        public IMongoCollection<Transaction> transactionsCollection;

        public Connect()
        {
            mongoClient = new MongoClient(connectionString);
            database = mongoClient.GetDatabase(databaseName);

            customersCollection = database.GetCollection<Customer>("customers");
            accountsCollection = database.GetCollection<Account>("accounts");
            transactionsCollection = database.GetCollection<Transaction>("transactions");

            Customers=new List<Customer>();
            Accounts=new List<Account>();
            Transactions=new List<Transaction>();



        }
    }
}
