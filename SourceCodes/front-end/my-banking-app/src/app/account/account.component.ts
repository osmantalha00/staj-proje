import { Component } from '@angular/core';
import { Location, formatCurrency } from '@angular/common';
import { AccountService } from './account.service'; 
import { Account } from './account.model';

import { NgForm } from '@angular/forms';
import { ViewChild } from '@angular/core';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})


export class AccountComponent {
  selectedAccount: Account | null = null;
  newAccount: Account = {}; // Yeni eklenen account için
  accounts: Account[] = []; // Tüm hesapları tutmak için
  accountsLoaded = false; // Hesapların yüklenip yüklenmediğini kontrol etmek için
  errorMessage: string | null = null;
  successMessage: string | null = null;
  @ViewChild('accountForm') accountForm!: NgForm;

  constructor(
    private location: Location,
    private accountService: AccountService
  ) { }

  goBack(): void {
    this.location.back();
  }

  addAccount(): void {
    // Formun tüm kontrollerini 'touched' yaptımm
    for (const controlName in this.accountForm.controls) {
      this.accountForm.controls[controlName].markAsTouched();
    }

    // Eğer form geçerli değilse bir hata mesajı göster ve fonksiyondan çık
    if (!this.accountForm.valid) {
        this.errorMessage = 'Lütfen tüm zorunlu alanları doldurunuz.';
        return;
    }

    // Başlangıçta her iki mesajı da sıfırla
    this.successMessage = null;
    this.errorMessage = null;

    console.log('Yeni hesap verisi:', this.newAccount); 

    this.accountService.addAccount(this.newAccount).subscribe(account => {
      console.log('Account added:', account); 
      this.loadAccounts(); 
      this.successMessage = 'Hesap başarılı şekilde eklendi!';
      setTimeout(() => {
        this.successMessage = null;
      }, 3000);  
      // Formu sıfırla
      this.newAccount = {
        customer_id: null,
        hesap_turu: null,
        acilis_tarihi: null,
        bakiye: null
      };
      this.accountForm.reset();
    }, 
    error => {
      
      console.error('Hesap eklenirken bir hata oluştu:', error);
      this.errorMessage = 'Hesap eklenirken bir hata oluştu. Lütfen daha sonra tekrar deneyin.';
    });
}



  // Tüm hesapları yüklemek için
  loadAccounts(): void {
    console.log("test2");
    this.accountService.getAllAccounts().subscribe(accounts => {
      this.accounts = accounts;
      this.accountsLoaded = true;
    });
  }

  // Hesabı güncellemek için
  editAccount(account: Account): void {
    this.selectedAccount = account;
    this.newAccount = { ...account };  // mevcut hesabın bilgilerini newAccount'a kopyaladım
  }

  // Hesabı silmek için
  deleteAccount(account: Account): void {
    this.accountService.deleteAccount(account.account_id!).subscribe(() => {
      console.log('Account deleted:', account);
      this.loadAccounts(); // Tüm hesapları yeniden yüklemek için
    });
  }

  updateAccount(): void {
    console.log('Güncellenen hesap verisi:', this.newAccount);

    // Seçili hesabın _id değerini koruyarak yeni hesaba aktarma kısmını ayarladım
    if (this.selectedAccount && this.selectedAccount._id) {
      this.newAccount._id = this.selectedAccount._id;
    }

    this.accountService.updateAccount(this.newAccount).subscribe(() => {
      console.log('Account updated:', this.newAccount);
      this.loadAccounts();  
      this.successMessage = 'Hesap başarılı şekilde güncellendi!';
      setTimeout(() => {
        this.successMessage = null;
      }, 3000);  

      // Formu sıfırla ve düzenleme modunu kapat
      this.newAccount = {
        customer_id: null,
        hesap_turu: null,
        acilis_tarihi: null,
        bakiye: null
      };

      if (this.accountForm) {
        this.accountForm.resetForm();
      }


      this.selectedAccount = null; // Seçili hesabı sıfırlayarak düzenleme modunu kapat
    });
  }


}































/*

import { Component } from '@angular/core';
import { Location } from '@angular/common';
@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent {
  constructor(  private location: Location) { }


  goBack(): void { 
    this.location.back();
  }


}









*/