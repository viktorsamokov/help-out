import { Injectable } from '@angular/core';
import { Http, RequestOptions, Response, Headers } from "@angular/http";
import { Observable } from 'rxjs/Observable';
import { Tag } from './tag.model';

@Injectable()
export class TagService {

  constructor(private http: Http) { }

  search ( term :String ): Observable<Tag[]> {
    if(term === ""){
        return Observable.of([]);
    }

    return this.http.get("/api/tags/search?term=" + term, this.jwt())
                    .map((response: Response) => {
                        let resp = response.json();
                        console.log(resp);
                        return resp || {};
                    })
  }
  
  // private helper
  private jwt(){
    let user = JSON.parse(localStorage.getItem('currentUser'));
    let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer '+ user.token });
    return new RequestOptions({ headers: headers });
  }

}
