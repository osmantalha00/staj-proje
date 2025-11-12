// account.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Account } from './account.model';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private apiUrl = 'http://localhost:5096/api/accounts';

  constructor(private http: HttpClient) {}

  getAllAccounts(): Observable<Account[]> {
     console.log("testssss");
    
    return this.http.get<Account[]>(this.apiUrl);
    
    
  }

  getAccountById(id: number): Observable<Account> {
    return this.http.get<Account>(`${this.apiUrl}/${id}`);
  }

  addAccount(account: Account): Observable<Account> {
    console.log('Adding account:', account); // İstek öncesi nesneyi konsola yazdır
    return this.http.post<Account>(this.apiUrl, account);
  }
  
  updateAccount(account: Account): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${account.account_id}`, account);
  }

  deleteAccount(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
