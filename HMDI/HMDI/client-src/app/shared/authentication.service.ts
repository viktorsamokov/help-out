import { Injectable } from '@angular/core';
import { Http, RequestOptions, Response, Headers } from "@angular/http";
import { Login } from "../login/login.model";
import { CurrentUser } from "./current-user.model";
import { BehaviorSubject } from "rxjs/BehaviorSubject";
import { Router } from "@angular/router";

@Injectable()
export class AuthenticationService {
  private userSource = new BehaviorSubject<CurrentUser>(JSON.parse(localStorage.getItem('currentUser')));
  
  currentUser = this.userSource.asObservable();

  constructor(private http: Http, private router: Router) { }

  login(loginModel: Login){
    return this.http.post("/api/users/authenticate", loginModel, this.jwt).map((response: Response) => {
      let user = response.json();
      if (user && user.token) {
          this.userSource.next(user as CurrentUser);
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          localStorage.setItem('currentUser', JSON.stringify(user));
      }
      return response.json();
    });
  }

  isAuthenticated(){
    return localStorage.getItem('currentUser');
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    this.router.navigate(['welcome']);
    this.userSource.next(null);
  }

  // private helper
  jwt(){
    let headers = new Headers({ 'Content-Type': 'application/json' });
    return new RequestOptions({ headers: headers });
  }
}
