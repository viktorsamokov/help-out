import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from "@angular/http";
import { AgendaCategory } from "./agenda-category.model";
import { Observable } from "rxjs/Observable";
import { Agenda } from "./category-agendas/category-agenda.model";

@Injectable()
export class AgendasService {
  agendaCategories: AgendaCategory[] = null;
  agendas: any = {};
  constructor(private http: Http) { }

  getUserAgendaCategories(): Observable<AgendaCategory[]>{
    if(this.agendaCategories != null){
      return Observable.of(this.agendaCategories);
    }

    return this.http.get("/api/agendacategories/user", this.jwt()).map((response: Response) => {
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

    return this.http.get("/api/agendas/category?id=" + id, this.jwt()).map((response : Response) => {
      let categoryAgendas = response.json();
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

  createAgendaCategory(agendaCategory): Observable<AgendaCategory>{
    return this.http.post("/api/agendacategories", agendaCategory, this.jwt()).map((response: Response) => {
      let category = response.json();
      if(this.agendaCategories == null){
        this.agendaCategories = [];
      }
      this.agendaCategories.push(category);
      return category;
    });
  }

  createAgenda(agenda): Observable<Agenda>{
    return this.http.post("/api/agendas", agenda, this.jwt()).map((response: Response) => {
      console.log(response);
      let resp = response.json();
      this.agendas[resp.agendaCategoryId].push(resp);
      let cate = this.agendaCategories.find(cat => {
        return cat.Id == resp.AgendaCategoryId;
      });
      cate.AgendasCount++;
      return resp;
    });
  }

  // private helper
  jwt(){
    let headers = new Headers({ 'Content-Type': 'application/json' });
    return new RequestOptions({ headers: headers });
  }
}
