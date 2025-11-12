import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Transaction } from './transaction.model'; 
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  private apiUrl = 'http://localhost:5096/api/transactions'; // API urlim

  constructor(private http: HttpClient) { }

  getTransactions(): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(this.apiUrl);
  }

  getTransaction(id: number): Observable<Transaction> {
    return this.http.get<Transaction>(`${this.apiUrl}/${id}`);
  }

  addTransaction(transaction: Transaction): Observable<void> {
    return this.http.post<void>(this.apiUrl, transaction);
  }

  updateTransaction(transaction: Transaction): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${transaction.transaction_id}`, transaction);
  }
  

/*

  updateTransaction(id: number, transaction: Transaction): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, transaction);
  }



*/
  
  deleteTransaction(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
