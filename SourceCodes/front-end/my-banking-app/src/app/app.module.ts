import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms'; 
import { HttpClientModule } from '@angular/common/http'; 
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CustomerComponent } from './customer/customer.component';
import { AccountComponent } from './account/account.component';
import { TransactionComponent } from './transaction/transaction.component';
import { CustomerService } from './customer/customer.service';
import { LoginComponent } from './login/login.component';

import { ControlBaseComponent } from './control-base/control-base.component'; 

@NgModule({
  declarations: [
    AppComponent,
    CustomerComponent,
    AccountComponent,
    TransactionComponent,
    LoginComponent,

    ControlBaseComponent
  ],
  imports: [
    ReactiveFormsModule,
    BrowserModule,
    AppRoutingModule,
    FormsModule, 
    HttpClientModule 
    
  ],
  providers: [
    CustomerService 
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
