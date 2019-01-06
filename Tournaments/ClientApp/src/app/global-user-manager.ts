import { Component, OnInit, Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class GlobalUserManager {
  public id: number;
  public name: string;
  private password: string;
  private email: string;
  private language: string;

  private locale: Object;

  constructor(private http: HttpClient) { }

  setUser(user: User) {
    if (this.language !== user.language) {
      this.http.get("./assets/locale/" + user.language + ".json").subscribe(data => {
        this.locale = data;
        console.log(this.locale);
      });
    }

    this.id = user.id;
    this.name = user.name;
    this.password = this.hash(user.password);
    this.email = user.email;
    this.language = user.language;
  }
  getUser() {
    return { id: this.id, name: this.name, email: this.email, password: this.password, language: this.language };
  }
  isLoggedIn() {
    return this.name !== undefined;
  }

  get(path: string, callback: Function) {
    this.http.get(location.origin + "/api/" + path).subscribe(
      obj => {
        callback(obj);
      }, error => console.error(error));
  }

  post(path: string, obj: object, callback: Function) {
    if (this.name === "" || this.name === null || this.name === undefined)
      return;

    this.http.post<Object>(location.origin + "/api/" + path, obj).subscribe(
      obj => {
        if (callback != null)
          callback(obj);
      }, error => console.error(error));
  }

  put(path: string, obj: object) {
    if (this.name === "" || this.name === null || this.name === undefined)
      return;
    this.http.put<Object>(location.origin + "/api/" + path, obj).subscribe(
      obj => {
        if (obj !== null)
          console.log(obj);
      }, error => console.error(error));
  }

  delete(path: string) {
    if (this.name === "" || this.name === null || this.name === undefined)
      return;
    this.http.delete(location.origin + "/api/" + path).subscribe(
      obj => {
        if (obj !== null)
          console.log(obj);
      }, error => console.error(error));
  }

  hash(str: string) {
    return str;
  }

  translate(key: string) {
    return this.locale[key];
  }
}

export interface User {
  id: number;
  name: string;
  email: string;
  password: string;
  language: string;
}
