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

    this.checklist.Items.push(item);
    this.checklistItem = null;
  }

  removeChecklistItem(index){
    this.checklist.Items.splice(index, 1);
  }

  open(data){
    this.checklistModal.show();
  }

  onDateChanged(event: IMyDateModel): void {
    // date selected
    console.log(event);
  }

  save(){
    if(this.date.jsdate && this.time){
      let dueDate = new Date(this.date.date.year, this.date.date.month - 1, this.date.date.day, this.time.hour, this.time.minute);
      this.checklist.DueDate = dueDate;
    }

    this.plannerService.saveChecklist(this.checklist).subscribe(val => {
      this.checklistModal.hide();
    });
    
  }

  public onHidden():void {
    this.checklist = new Checklist();
    this.date = null;
    this.time = {hour: 0, minute: 0};
  }
}
