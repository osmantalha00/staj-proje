import { Component, OnInit } from '@angular/core';
import { Customer } from './customer.model';
import { CustomerService } from './customer.service';
import { Location } from '@angular/common';
import { NgForm } from '@angular/forms';
import { ViewChild } from '@angular/core';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {
  customers: Customer[] = [];
  newCustomer: Customer = new Customer();
  errorMessage: string = '';
  customersLoaded = false; // Müşterilerin yüklenip yüklenmediğini kontrolüü
  successMessage: string | null = null;
  @ViewChild('customerForm') customerForm!: NgForm;

  
  constructor(
    private customerService: CustomerService,
    private location: Location 
  ) { }

  ngOnInit(): void {
    // Başlangıçta müşterileri yükleme
  }

  loadCustomers(): void {
    this.errorMessage = '';
    this.customerService.getCustomers().subscribe(
      (data: Customer[]) => {
        this.customers = data;
        this.customersLoaded = true; // Müşteriler yüklendi 
      },
      (error) => {
        console.error('API isteği başarısız oldu:', error);
        this.errorMessage = 'Müşteriler yüklenirken bir hata oluştu. Lütfen daha sonra tekrar deneyin.';
      }
    );
  }

  addCustomer(): void {
    // Formun tüm kontrollerini 'touched' olarak işaretlediğimiz yer burasıı
    for (const controlName in this.customerForm.controls) {
      this.customerForm.controls[controlName].markAsTouched();
    }

    if (!this.customerForm.valid) {
      console.log("form geçerli değil miiiiii ")
        this.errorMessage = 'Lütfen tüm zorunlu alanları doldurunuz.';
        return;  // Form geçerli değilse, fonksiyondan çık
    }

    // Başlangıçta her iki mesajı da sıfırla
    this.successMessage = null;
    this.errorMessage = '';

    this.customerService.addCustomer(this.newCustomer).subscribe(
      () => {
        this.refreshCustomers();  // Tüm müşterileri yeniden yükle
        this.newCustomer = new Customer();  // Yeni müşteri formunu sıfırla
        this.customerForm.resetForm();  // Formu sıfırla
        this.errorMessage = '';  // Hata mesajını temizle
        this.successMessage = 'Müşteri başarılı şekilde eklendi!';

        
        setTimeout(() => {
            this.successMessage = null;
        }, 3000);
      },
      error => {
        
        console.error('Müşteri eklenirken bir hata oluştu:', error);
        this.errorMessage = 'Müşteri eklenirken bir hata oluştu. Lütfen daha sonra tekrar deneyin.';
      }
    );
}


editCustomer(customer: Customer): void {
  this.newCustomer = { ...customer }; // Mevcut müşteri bilgilerini kopyala

  if (customer.customer_id) {
    this.deleteCustomer(customer);         // şurasıııı bakarsın 
  }
}

  /*updateCustomer(): void {
    this.customerService.updateCustomer(this.newCustomer).subscribe(
      () => {
        this.refreshCustomers();
        this.newCustomer = new Customer(); // Formu sıfırla
      },
      (error) => {
        console.error('Müşteri güncellenirken bir hata oluştu:', error);
        this.errorMessage = 'Müşteri güncellenirken bir hata oluştu. Lütfen daha sonra tekrar deneyin.';
      }
    );
  }
  
  */


  /*

addOrEditCustomer(): void {
  if (this.newCustomer.customer_id) {
    this.updateCustomer(); // Müşteri güncelleme
  } else {
    this.addCustomer(); // Yeni müşteri ekleme
  }
}


  */
  

  deleteCustomer(customer: Customer): void {
    if (customer.customer_id) {
      this.customerService.deleteCustomer(customer.customer_id).subscribe(() => {
        this.refreshCustomers();
      });
    } else {
      
      this.errorMessage = 'Müşteri silinirken bir hata oluştu. Lütfen daha sonra tekrar deneyin.';
    }
  }

  refreshCustomers(): void {
    this.loadCustomers(); // Müşterileri yeniden yükleme
  }

  goBack(): void {
    this.location.back();
  }
}







// console log ( TEST ); 







/*


import { Component, OnInit } from '@angular/core';
import { Customer } from './customer.model';
import { CustomerService } from './customer.service';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {
  customers: Customer[] = [];
  newCustomer: Customer = new Customer();
  errorMessage: string = ''; // Hata mesajı için değişken ekledik
  constructor(private customerService: CustomerService) { }

  ngOnInit(): void {
    this.loadCustomers();
  }

  loadCustomers(): void {
    this.errorMessage = ''; // Her istek öncesi hata mesajını sıfırlayın
    this.customerService.getCustomers().subscribe(
      (data: Customer[]) => {
        this.customers = data;
      },
      (error) => {
        console.error('API isteği başarısız oldu:', error);
        this.errorMessage = 'Müşteriler yüklenirken bir hata oluştu. Lütfen daha sonra tekrar deneyin.'; // Hata mesajını ayarlayın
      }
    );
  }

  addCustomer(): void {
    this.customerService.addCustomer(this.newCustomer).subscribe(
      () => {
        this.loadCustomers();
        this.newCustomer = new Customer();
      },
      (error) => {
        console.error('Müşteri eklenirken bir hata oluştu:', error);
        this.errorMessage = 'Müşteri eklenirken bir hata oluştu. Lütfen daha sonra tekrar deneyin.'; // Hata mesajını ayarlayın
      }
    );
  }
  editCustomer(customer: Customer): void {
    // Implement edit logic here
  }

  deleteCustomer(customer: Customer): void {
    if (typeof customer.customer_id === 'number') { // customer_id'nin number tipinde olup olmadığını kontrol edin
      this.customerService.deleteCustomer(customer.customer_id).subscribe(() => {
        this.loadCustomers();
      });
    } else {
      // customer_id tanımlanmamış durumda, uygun bir işlem veya hata mesajı ekle usmanım
    }
  }
}
























*/