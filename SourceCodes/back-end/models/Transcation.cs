using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Project
{
    public class Transaction
    {
        public ObjectId Id { get; set; }

        [Required(ErrorMessage = "Transaction Id boş bırakılamaz.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Transaction Id'de harf olamaz.")]
        public int transaction_id { get; set; }

        public int account_id { get; set; }

        public DateTime tarih { get; set; }

        [Required(ErrorMessage = "Tutar boş bırakılamaz.")]
       // [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Tutar değeri geçerli bir sayı olmalıdır.")]




        public decimal tutar { get; set; }

        [Required(ErrorMessage = "İşlem türü boş bırakılamaz.")]
        public string islem_turu { get; set; }
    }
}

// trancation id boş bırakılmaz 
// transcation ıd harf girilemez 
// tutar boş bırakılamaz 
// tutar karakter içeremez 
// işlem türü boş bırakılmaz 


// account id kontrolü sağlanmadı oatamtaik atama var 

// bide puta bak getlerde sorun varsa düzlet 

/*

 [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Customer customer)
        {
            if(id != customer.customer_id)
            {
                return BadRequest(new { error = "Gönderilen müşteri kimliği URL'deki kimlikle eşleşmiyor." });
            }
            try
            {
*/