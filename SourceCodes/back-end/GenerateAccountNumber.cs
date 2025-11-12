using System;

namespace Project{

    public class AccountNumberGenerator
    {
        public static string GenerateAccountNumber()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString().Replace("-", "");
        }
    }

 }