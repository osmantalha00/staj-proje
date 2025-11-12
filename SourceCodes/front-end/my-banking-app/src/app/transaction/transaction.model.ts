export class Transaction {
    Id?: string; // MongoDB için ObjectId, gerekirse kullanılır
    transaction_id?: number;
    account_id?: number;
    tarih?: Date;
    tutar?: number;
    islem_turu?: string;
    
  }
  