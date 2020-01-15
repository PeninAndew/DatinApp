import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { tokenName } from '@angular/compiler';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  model: any = {};
  constructor(public authService: AuthService, private alertify: AlertifyService) {}

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
    this.alertify.success('logged in successfuly');
    }, error => {
      this.alertify.error('Should say ERROR!!!');
    });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    this.alertify.message('logged out');
  }

}
