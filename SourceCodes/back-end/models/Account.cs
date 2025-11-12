using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace Project
{
    public class Account
    {
        public ObjectId Id { get; set; }

        public int account_id { get; set; } 

        [Required(ErrorMessage = "Customer Id is required.")]                                  // string path = @"C:\Users\Example\Documents";

        [RegularExpression(@"^\d+$", ErrorMessage = "Customer Id must be a number.")]
        public int customer_id { get; set; }

        [Required(ErrorMessage = "Account type is required.")]
        public string hesap_turu { get; set; }

        public DateTime acilis_tarihi { get; set; }

        [Required(ErrorMessage = "bakiye bilgisi boş bırakılamaz .")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = " bakiye bilgisinde  harf olamaz.")]   // ^[0-9]+(\.[0-9]{1,2})?$

        public decimal? bakiye { get; set; } // hesaptaki toplam bakiye 
    }
}

// nelere kontrol sağlanmalı 
//public decimal bakiye { get; set; }
// costumer ıd boş bırakılmamalı 
// costumer ıd harf içeremez 
// hesap türü boş bırakılmamalı 
// bakiye bilgisi girilmeli 
