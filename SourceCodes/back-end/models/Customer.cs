using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Project
{
    public class Customer
    {
        public ObjectId Id { get; set; }
        public int customer_id { get; set; } // objected ıd ile gerekli işlemler yapılsın düzenleme yap 

        [Required(ErrorMessage = "Müşteri adı boş bırakılamaz.")]
        [RegularExpression(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ]+$", ErrorMessage = "Müşteri adında sadece harfler bulunabilir.")]
        public string ad { get; set; }


        [Required(ErrorMessage = "Müşteri soyadı boş bırakılamaz.")]
        public string soyad { get; set; }

        public string adres { get; set; }

        [Required(ErrorMessage = "Müşteri telefonu boş bırakılamaz.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Müşteri telefonunda harf olamaz.")]
        public string telefon { get; set; }

        [Required(ErrorMessage = "Müşteri e-posta adresi boş bırakılamaz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
        public string eposta { get; set; }
    }
}


// nelere kontrol sağlanmalı 
// müşteri adı boş girilmez 
// müşteri adında rakam olamaz
// müşteri soyadı boş girilmez
//  müşteri telefon boş olamaz   
//  müşteri telefon da harf olmaz
//  müşteri e posta boş girilmrz 