import { Injectable } from '@angular/core';
import { Agenda } from '../agendas/category-agendas/category-agenda.model';
import { FavoriteAgenda } from './favorite-agenda.model';
import { Observable } from 'rxjs/Observable';
import { Http, Response, RequestOptions, Headers } from '@angular/http';

@Injectable()
export class FavoritesService {
  favorites: FavoriteAgenda[] = null;
  
  constructor(private http: Http) { }

  getFavorites(): Observable<FavoriteAgenda[]>{
    if(this.favorites != null){
      return Observable.of(this.favorites);
    }

    return this.http.get("/api/users/favoriteagendas", this.jwt()).map((response: Response) => {
        let resp = response.json();
        this.favorites = new Array<FavoriteAgenda>();
        resp.forEach(element => {
          element.state = "inactive";
          this.favorites.push(element);
        });
        return resp;
      });
  }

  rateAgenda(agenda): Observable<FavoriteAgenda>{
    console.log(agenda);
    return this.http.post("/api/agendas/rateagenda", agenda, this.jwt()).map((response: Response) => {
      let resp = response.json();
      return resp;
    });
  }

  addToFavorites(favorite){
    if(this.favorites){
      this.favorites.push(favorite);
    }
  }

  // private helper
  private jwt(){
    let headers = new Headers({ 'Content-Type': 'application/json' });
    return new RequestOptions({ headers: headers });
  }
}
