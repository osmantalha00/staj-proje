namespace Project{
    class Insertion{




        public  void addCustomer(String name,String surname,String adres,String ph,String mail,IdGenerator idGenerator,Connect conn){
    
        Customer customer = new Customer
        {
            customer_id = idGenerator.GenerateCustomerId(),
            ad = name,
            soyad = surname,
            adres = adres,
            telefon = ph,
            eposta = mail
        };
        conn.customersCollection.InsertOne(customer);
        conn.Customers.Add(customer);
        

    }

    public void addTransaction(String type,decimal amount, IdGenerator idGenerator,Connect conn,Account account){

         Transaction transaction = new Transaction
        {
            transaction_id = idGenerator.GenerateTransactionId(),
            account_id = account.account_id,
            tarih = DateTime.Now,
            tutar = 500,
            islem_turu = "Çekme"
        };
        conn.transactionsCollection.InsertOne(transaction);
        conn.Transactions.Add(transaction);
        

    }

     public  void addAccount(String type,decimal amount,IdGenerator idGenerator,Connect conn,Customer customer){
  
        Account account = new Account
        {
            account_id = idGenerator.GenerateAccountId(),
            customer_id = customer.customer_id,
            hesap_turu = type,
            acilis_tarihi = DateTime.Now,
            bakiye = amount
        };
        conn.accountsCollection.InsertOne(account);
        conn.Accounts.Add(account);
        

    }
    }
}