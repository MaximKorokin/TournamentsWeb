import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GlobalUserManager, User } from '../global-user-manager';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  name: string;
  email: string;
  password1: string;
  password2: string;

  url: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private manager: GlobalUserManager) {
    this.url = baseUrl + "api/Users";
  }

  ngOnInit() {
  }

  signUp() {
    if (this.password1 != this.password2) {
      console.log("passwords are different");
      return;
    }
    this.manager.setUser({id: 0, name: this.name, email: this.email, password: this.password1 });
    let user = this.manager.getUser();
    this.http.post<User>(this.url, user).subscribe(
      user => {
        console.log(user.email);
      });
  }
}
