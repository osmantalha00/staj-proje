import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomerComponent } from './customer/customer.component';
import { AccountComponent } from './account/account.component';
import { TransactionComponent } from './transaction/transaction.component';
import { LoginComponent } from './login/login.component';
import { ControlBaseComponent } from './control-base/control-base.component'; 

const routes: Routes = [
  { path: 'customer', component: CustomerComponent },
  { path: 'account', component: AccountComponent },
  { path: 'transaction', component: TransactionComponent },
  { path: 'login', component: LoginComponent },
  { path: 'control-base', component: ControlBaseComponent }, 
  { path: '', redirectTo: '/login', pathMatch: 'full' }, // varsayılan rota giriş ekranına yönlendirir
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

