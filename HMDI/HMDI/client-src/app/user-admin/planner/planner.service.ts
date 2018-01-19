import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Checklist } from './checklist.model';
import { Observable } from 'rxjs/Observable';
import { ChecklistItem } from './checklist-item.model';

@Injectable()
export class PlannerService {
  todayTasks: Checklist[] = null;
  weeklyTasks: Checklist[] = null;
  activeTasks: Checklist[] = null;

  constructor(private http: Http) { }

  getTodayTasks(): Observable<Checklist[]>{
    if(this.todayTasks != null){
      return Observable.of(this.todayTasks);
    }

    return this.http.get("/api/checklists/daily", this.jwt()).map((response: Response) => {
      let tasks = response.json();
      if(this.todayTasks == null){
        this.todayTasks = new Array<Checklist>();
      }
      tasks.forEach(element => {
        this.todayTasks.push(element);
      });
      return tasks;
    });
  }

  getWeeklyTasks(): Observable<Checklist[]>{
    if(this.weeklyTasks != null){
      return Observable.of(this.weeklyTasks);
    }

    return this.http.get("/api/checklists/weekly", this.jwt()).map((response: Response) => {
      let tasks = response.json();
      if(this.weeklyTasks == null){
        this.weeklyTasks = [];
      }
      tasks.forEach(element => {
        this.weeklyTasks.push(element);
      });
      return tasks;
    });
  }

  getActiveTasks(): Observable<Checklist[]>{
    if(this.activeTasks != null){
      return Observable.of(this.activeTasks);
    }

    return this.http.get("/api/checklists/active", this.jwt()).map((response: Response) => {
      let tasks = response.json();
      if(this.activeTasks == null){
        this.activeTasks = [];
      }
      tasks.forEach(element => {
        this.activeTasks.push(element);
      });
      return tasks;
    });
  }

  saveChecklist(checklist): Observable<Checklist>{
    return this.http.post("/api/checklists", checklist, this.jwt()).map((response: Response) => {
      let resp = response.json();
      this.handleChecklist(resp);
      
      return resp;
    });
  }

  updateChecklist(checklist): Observable<Checklist>{
    return this.http.put("/api/checklists/" + checklist.Id, checklist, this.jwt()).map((response: Response) => {
      let resp = response.json();
      if(!resp.IsFinished){
        this.handleChecklist(resp);
      }
      
      return resp;
    });
  }

  updateChecklistItem(checklistItem): Observable<ChecklistItem>{
    return this.http.put("/api/checklistItems/" + checklistItem.Id, checklistItem, this.jwt()).map((response: Response) => {
      return checklistItem;
    });
  }

  deleteChecklist(checklist): Observable<ChecklistItem>{
    return this.http.delete("/api/checklists/" + checklist.Id, this.jwt()).map((response: Response) => {
      return checklist;
    });
  }

  removeFromDaily(id){
    let index = this.todayTasks.findIndex(val => val.Id == id);
    this.todayTasks.splice(index, 1);
  }

  removeFromWeekly(id){
    let index = this.weeklyTasks.findIndex(val => val.Id == id);
    this.weeklyTasks.splice(index, 1);
  }

  removeFromActive(id){
    let index = this.activeTasks.findIndex(val => val.Id == id);
    this.activeTasks.splice(index, 1);
  }

  // private helper
  jwt(){
    let user = JSON.parse(localStorage.getItem('currentUser'));
    let headers = new Headers({ 'Content-Type': 'application/json', 'Authorization': 'Bearer '+ user.token });
    return new RequestOptions({ headers: headers });  }

  handleChecklist(resp){
    let today = new Date();
    today.setHours(0, 0);
    let tomorrow = new Date();
    tomorrow.setHours(23, 59);
    if(!resp.DueDate || new Date(resp.DueDate) < today ){
      if(this.activeTasks){
        this.activeTasks.push(resp);          
      }
    }
    else if(new Date(resp.DueDate) > tomorrow){
      if(this.weeklyTasks){
        this.weeklyTasks.push(resp);          
      }
    }
    else {
      if(this.todayTasks){
        this.todayTasks.push(resp);          
      }
    }
  }

}
