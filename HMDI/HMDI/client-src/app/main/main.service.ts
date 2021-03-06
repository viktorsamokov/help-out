import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Agenda } from '../user-admin/agendas/category-agendas/category-agenda.model';
import { Http, Response, RequestOptions, Headers } from '@angular/http';

@Injectable()
export class MainService {

  constructor(private http: Http) { }

  searchByTags(tags): Observable<Agenda[]>{
    return this.http.post("/api/agendas/searchbytags", tags, this.jwt()).map((response: Response) => {
        let resp = response.json();
        return resp;
      });
  }

  searchByName(name): Observable<Agenda[]>{
    return this.http.get("/api/agendas/searchbyname?name=" + name, this.jwt()).map((response: Response) => {
        let resp = response.json();
        return resp;
      });
  }

  addToFavorites(agenda): Observable<any>{
    return this.http.post("/api/agendas/favorite", agenda, this.jwt()).map((response: Response) => {
      let resp = response.json();
      return resp;
    });
  }

  // private helper
  private jwt(){
    let user = JSON.parse(localStorage.getItem('currentUser'));
    let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer '+ user.token });
    return new RequestOptions({ headers: headers });  }
}
