import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { TransactionService } from './transaction.service'; // Service'i içe aktarma
import { Transaction } from './transaction.model';
import { NgForm } from '@angular/forms';
import { ViewChild } from '@angular/core';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.css']
})
export class TransactionComponent implements OnInit {
  newTransaction: Transaction = {}; // Yeni eklenen işlem için
  transactions: Transaction[] = []; // Tüm işlemleri tutmak için
  successMessage: string | null = null;
  errorMessage: string | null = null;
  selectedTransaction: Transaction | null = null;
  @ViewChild('transactionForm') transactionForm!: NgForm;

  constructor(
    private location: Location,
    private transactionService: TransactionService 
  ) { }

  ngOnInit(): void {
    this.loadTransactions(); // İlk başta tüm işlemleri yükle
  }

  goBack(): void {
    this.location.back();
  }

  // İşlem eklemek için
  addTransaction(): void {
    this.transactionService.addTransaction(this.newTransaction).subscribe(
      () => {
        console.log('Transaction added:', this.newTransaction);
        this.loadTransactions();

        this.successMessage = 'İşlem başarıyla eklendi!';
        setTimeout(() => {
          this.successMessage = null;
        }, 3000);  

        this.newTransaction = {};
        if (this.transactionForm) {
          this.transactionForm.resetForm();
        }
      },
      (error) => {
        console.error('İşlem eklenirken bir hata oluştu:', error);
        this.errorMessage = 'İşlem eklenirken bir hata oluştu. Lütfen daha sonra tekrar deneyin.';
        setTimeout(() => {
          this.errorMessage = null;
        }, 5000);
      }
    );
}


  // Tüm işlemleri yüklemek için
  loadTransactions(): void {
    this.transactionService.getTransactions().subscribe(transactions => {
      this.transactions = transactions;
    });
  }

  // İşlemi silmek için
  deleteTransaction(transaction: Transaction): void {
    this.transactionService.deleteTransaction(transaction.transaction_id!).subscribe(() => {
      console.log('Transaction deleted:', transaction);
      this.loadTransactions(); // Tüm işlemleri yeniden yüklemek için
    });
  }

  editTransaction(transaction: Transaction): void {        // edit Transcation 
    this.selectedTransaction = transaction;
    this.newTransaction = { ...transaction };
  }




  updateTransaction(): void {
    console.log('Güncellenen işlem verisi:', this.newTransaction);

    // Eğer seçili işlem varsa ve bu işlemin transaction_id değeri varsa, bu değeri koruyarak yeni işleme aktar
    if (this.selectedTransaction && this.selectedTransaction.transaction_id) {
      this.newTransaction.transaction_id = this.selectedTransaction.transaction_id;
    }

    this.transactionService.updateTransaction(this.newTransaction).subscribe(() => {
      console.log('Transaction updated:', this.newTransaction);
      this.loadTransactions();  // Güncelleme sonrası tüm işlemleri yeniden yükle
      this.successMessage = 'İşlem başarılı şekilde güncellendi!';
      setTimeout(() => {
        this.successMessage = null;
      }, 3000);  

      // Formu sıfırla ve düzenleme modunu kapat
      this.newTransaction = {
        transaction_id: undefined,
        account_id: undefined,
        tarih: undefined,
        tutar: undefined,
        islem_turu: undefined
      };

      if (this.transactionForm) {
        this.transactionForm.resetForm();
      }

      this.selectedTransaction = null; // Seçili işlemi sıfırlayarak düzenleme modunu kapat
    });
  }











}

