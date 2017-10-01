import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Http, Headers, Response, RequestOptions } from "@angular/http";
import { Register } from "./register.model";

@Injectable()
export class RegisterService {

  constructor(private http: Http) { }

  registerUser(user: Register){
    return this.http.post('/api/users', user, this.jwt()).map((response: Response) => {
      return response.json();
    })
  }

  // private helper
  jwt(){
    let headers = new Headers({ 'Content-Type': 'application/json' });
    return new RequestOptions({ headers: headers });
  }
}
