import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  constructor(private router: Router) { }

  login(username: string, password: string) {
    const hardcodedUsername = 'talha';
    const hardcodedPassword = '123';

    if (username === hardcodedUsername && password === hardcodedPassword) {
      // Giriş başarılı, başka bir sayfaya yönlendirme yaptım
      this.goToPage('control-base'); // control base sayfasına aktardım 
    } else {
      // Giriş başarısız, kullanıcıya bilgi verildi
      alert('Kullanıcı adı veya şifre yanlış');
    }
  }

  goToPage(page: string): void {
    this.router.navigate([page]);
  }
}
