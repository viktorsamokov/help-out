import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from "@angular/http";
import { UserAgendaCategory } from "./user-agenda-category.model";
import { Observable } from "rxjs/Observable";
import { Agenda } from "./category-agendas/category-agenda.model";

@Injectable()
export class UserAgendaService {
  agendaCategories: UserAgendaCategory[] = null;
  agendas: any = {};
  constructor(private http: Http) { }

  getUserAgendaCategories(): Observable<UserAgendaCategory[]>{
    if(this.agendaCategories != null){
      return Observable.of(this.agendaCategories);
    }

    return this.http.get("/api/agendacategories/user", this.jwt).map((response: Response) => {
      let categories = response.json();
      if(this.agendaCategories == null){
        this.agendaCategories = [];
      }
      categories.forEach(element => {
        this.agendaCategories.push(element);
      });
      return response.json();
    });
  }

  getAgendasByCategory(id: number): Observable<Agenda[]>{
    if(this.agendas[id]){
      return Observable.of(this.agendas[id]);
    }

    return this.http.get("/api/agendas/category?id=" + id, this.jwt).map((response : Response) => {
      let categoryAgendas = response.json();
      console.log(categoryAgendas);
      this.agendas[id] = new Array<Agenda>();
      categoryAgendas.forEach(element => {
        this.agendas[id].push(element);
      });
      return this.agendas[id];
    });
  }

  removeAgendaCategory(){

  }

  removeAgenda(){

  }

  updateAgendaCategory(){

  }

  updateAgenda(){

  }

  createAgendaCategory(agendaCategory): Observable<UserAgendaCategory>{
    return this.http.post("/api/agendacategories", agendaCategory, this.jwt).map((response: Response) => {
      let category = response.json();
      console.log(category);
      if(this.agendaCategories == null){
        this.agendaCategories = [];
      }
      this.agendaCategories.push(category);
      return category;
    });
  }

  createAgenda(){
    
  }

  // private helper
  jwt(){
    let headers = new Headers({ 'Content-Type': 'application/json' });
    return new RequestOptions({ headers: headers });
  }
}
