import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'; 
import { Observable } from 'rxjs'; 
import { Customer } from './customer.model'; 
//  C:\Users\MONSTER\Desktop\vb_stajj\models
@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private apiUrl = 'http://localhost:5096/api/Customers'; // API URLim

  constructor(private http: HttpClient) { } 

  // Tüm müşterileri al
  getCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.apiUrl);
  } 

  // Belirli bir müşteriyi al
  getCustomer(id: number): Observable<Customer> {
    return this.http.get<Customer>(`${this.apiUrl}/${id}`);
  }

  // Yeni bir müşteri ekl
  addCustomer(customer: Customer): Observable<Customer> {
    return this.http.post<Customer>(this.apiUrl, customer);
  }

  // Müşteriyi güncelle
  updateCustomer(customer: Customer): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${customer.id}`, customer);
  }

  // Müşteriyi sil
  deleteCustomer(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
