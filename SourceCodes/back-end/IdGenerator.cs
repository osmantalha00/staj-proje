namespace Project{


    public class IdGenerator{
         private int customerId;
         private int accountId;
         private int transactionId;

          public IdGenerator()
        {
            customerId = 1;
            accountId = 1;
            transactionId = 1;
        }

        public int GenerateCustomerId()
        {
            return customerId++;
        }

        public int GenerateAccountId()
        {
            return accountId++;
        }

        public int GenerateTransactionId()
        {
            return transactionId++;
        }

    }

}