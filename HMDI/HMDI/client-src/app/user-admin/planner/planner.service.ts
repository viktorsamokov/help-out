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

  getTodayTasks(){
    if(this.todayTasks != null){
      return Observable.of(this.todayTasks);
    }

    return this.http.get("/api/checklists/daily", this.jwt()).map((response: Response) => {
      let tasks = response.json();
      if(this.todayTasks == null){
        this.todayTasks = [];
      }
      tasks.forEach(element => {
        this.todayTasks.push(element);
      });
      return tasks;
    });
  }

  getWeeklyTasks(){
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

  getActiveTasks(){
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
      console.log(resp);
      return resp;
    });
  }

  updateChecklist(checklist): Observable<Checklist>{
    return this.http.put("/api/checklists/" + checklist.Id, checklist, this.jwt()).map((response: Response) => {
      return checklist;
    });
  }

  updateChecklistItem(checklistItem): Observable<ChecklistItem>{
    return this.http.put("/api/checklistItems/" + checklistItem.Id, checklistItem, this.jwt()).map((response: Response) => {
      return checklistItem;
    });
  }

  // private helper
  jwt(){
    let headers = new Headers({ 'Content-Type': 'application/json' });
    return new RequestOptions({ headers: headers });
  }

}
