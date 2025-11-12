import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
@Component({
  selector: 'app-control-base',
  templateUrl: './control-base.component.html',
  styleUrls: ['./control-base.component.css']
})
export class ControlBaseComponent {

  constructor(private router: Router ,  private location: Location) { }

  goToCustomer() {
    this.router.navigate(['/customer']);
  }

  goToAccount() {
    this.router.navigate(['/account']);
  }

  goToTransaction() {
    this.router.navigate(['/transaction']);
  }

  goBack(): void { 
    this.location.back();
  }


}


