import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GlobalUserManager, User } from '../global-user-manager';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  name: string;
  password: string;

  url: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private manager: GlobalUserManager) {
    this.url = baseUrl + "api/Users/";
  }

  ngOnInit() {
  }

  login() {
    if (this.name === "" || this.password === "") {
      return;
    }
    this.http.get(this.url + this.name + "/" + this.manager.hash(this.password)).subscribe(
      u => {
        let user = <User>u;
        this.manager.setUser({ id: user.id, name: user.name, email: user.email, password: user.password, language: user.language });
        console.log(this.manager.getUser());
      }, error => console.error(error));
  }
}
