import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'angular-bootstrap-md';
import { INgxMyDpOptions, IMyDateModel } from 'ngx-mydatepicker';
import { Checklist } from '../planner/checklist.model';
import { PlannerService } from '../planner/planner.service';
import { ChecklistItem } from '../planner/checklist-item.model';

@Component({
  selector: 'app-checklist-modal',
  templateUrl: './checklist-modal.component.html',
  styleUrls: ['./checklist-modal.component.scss']
})
export class ChecklistModalComponent implements OnInit {
  
  @ViewChild('checklistModal') public checklistModal: ModalDirective;

  currentDate = new Date();
  myOptions: INgxMyDpOptions;
  checklist: Checklist;
  checklistItem: string;
  date: any;
  time = {hour: 0, minute: 0};
  meridian: false;
  plannerType: string;

  constructor(private plannerService: PlannerService) {
    this.checklist = new Checklist();
  }

  ngOnInit(): void {
    this.currentDate.setDate(this.currentDate.getDate() - 1);
    this.myOptions = {
      dateFormat: 'dd.mm.yyyy',
      disableUntil: {day: this.currentDate.getDay() + 1 , month: this.currentDate.getMonth() + 1, year: this.currentDate.getFullYear() }
    };
  }

  addChecklistItem(checklistItem){
    if(!checklistItem || checklistItem.length < 1){
      return;
    }

    if(!this.checklist.Items){
      this.checklist.Items = new Array<ChecklistItem>();
    }

    let item = new ChecklistItem();
    item.Todo = checklistItem;
    item.IsChecked = false;

    this.checklist.Items.push(item);
    this.checklistItem = null;
  }

  removeChecklistItem(index){
    this.checklist.Items.splice(index, 1);
  }

  open(data){
    console.log(data);
    if(data.task){
      this.checklist = JSON.parse(JSON.stringify(data.task));
      this.plannerType = data.planner;  

      if(data.task.DueDate){
        let dueDate = new Date(data.task.DueDate);
        this.time = {
           hour: dueDate.getHours(),
           minute: dueDate.getMinutes()
        }
        this.date = {
          date: {
            year: dueDate.getFullYear(),
            month: dueDate.getMonth() + 1,
            day: dueDate.getDate()}
        }
      }
    }
    this.checklistModal.show();
  }

  onDateChanged(event: IMyDateModel): void {
    // date selected
    console.log(event);
  }

  save(){
    if(this.checklist.Id){
      if(this.date && this.time){
        let dueDate = new Date(this.date.date.year, this.date.date.month - 1, this.date.date.day, this.time.hour, this.time.minute);
        this.checklist.DueDate = dueDate;
      }
      else {
        this.checklist.DueDate = null;        
      }
    }
    else {
      if(this.date && this.date.jsdate && this.time){
        let dueDate = new Date(this.date.date.year, this.date.date.month - 1, this.date.date.day, this.time.hour, this.time.minute);
        this.checklist.DueDate = dueDate;
      }
    }

    if(this.checklist.Id){
      let isLastChecked = this.checklist.Items.find(el => el.IsChecked == false);
      if(!isLastChecked){
        this.checklist.IsFinished = true;
      }

      this.plannerService.updateChecklist(this.checklist).subscribe(val => {
        if(this.plannerType === 'daily'){
          this.plannerService.removeFromDaily(this.checklist.Id);
        }
        else if(this.plannerType === 'weekly'){
          this.plannerService.removeFromWeekly(this.checklist.Id);
        }
        else if(this.plannerType === 'active'){
          this.plannerService.removeFromActive(this.checklist.Id);
        }
        this.checklistModal.hide();
      });
    }
    else {
      this.plannerService.saveChecklist(this.checklist).subscribe(val => {
        this.checklistModal.hide();
      });
    }
  }

  public onHidden():void {
    this.checklist = new Checklist();
    this.date = null;
    this.time = {hour: 0, minute: 0};
  }
}
