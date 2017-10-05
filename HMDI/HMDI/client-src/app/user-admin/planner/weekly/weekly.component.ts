import { Component, OnInit } from '@angular/core';
import { Checklist } from '../checklist.model';
import { PlannerService } from '../planner.service';
import { trigger, state, style, animate, transition } from '@angular/animations';
import { ModalsService } from '../../modals.service';

@Component({
  selector: 'app-weekly',
  templateUrl: './weekly.component.html',
  styleUrls: ['./weekly.component.scss'],
  animations: [
    trigger('animateHeight', [
        state('inactive', style({
          height: '0',
          opacity: 0
        })),
        state('active', style({
          height: '*',
          opacity: 1
        })),
        transition('inactive => active', animate('200ms ease-in')),
        transition('active => inactive', animate('200ms ease-out'))
    ])
  ]
})
export class WeeklyComponent implements OnInit {
  public tasks: Checklist[] = [];
  public today = new Date();
  public next7days = {
    first: new Date().setDate(this.today.getDate() + 1),
    second: new Date().setDate(this.today.getDate() + 2),
    third: new Date().setDate(this.today.getDate() + 3),
    forth: new Date().setDate(this.today.getDate() + 4),
    fifth: new Date().setDate(this.today.getDate() + 5),
    sixth: new Date().setDate(this.today.getDate() + 6),
    seventh: new Date().setDate(this.today.getDate() + 7)
  };

  constructor(private plannerService: PlannerService, private modalService: ModalsService) {  }

  ngOnInit() {
    this.plannerService.getWeeklyTasks().subscribe(val => {
      console.log(val);
      this.tasks = val;
      this.tasks.forEach(element => {
        if(!element.state){
          element.state = "inactive";
        }
      });
    });
  }

  toggleTask(task){
    if(!task.state){
      task.state = "active";
    }else {
      task.state = (task.state === 'inactive' ? 'active' : 'inactive');      
    }
  }

  toggleItem(item, task, taskIndex){
    let isLastChecked = task.Items.find(el => el.IsChecked == false);
    if(!isLastChecked){
      task.IsFinished = true;
      this.plannerService.updateChecklist(task).subscribe(val => {
        let index = this.tasks.findIndex(val => val.Id == task.Id);
        this.tasks.splice(index, 1);
      });
    }
    else {
      this.plannerService.updateChecklistItem(item).subscribe(val => {});
    }
  }

  openChecklistModal(task){
    let data = {task: task, planner: 'weekly'};
    this.modalService.openChecklistModal(data);
  }

  removeTask(task){
    this.plannerService.deleteChecklist(task).subscribe(val => {
      let index = this.tasks.findIndex(val => val.Id == task.Id);
      this.tasks.splice(index, 1);
    });
  }

}
