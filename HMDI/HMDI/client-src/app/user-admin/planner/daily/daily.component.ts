import { Component, OnInit } from '@angular/core';
import { PlannerService } from '../planner.service';
import { Checklist } from '../checklist.model';
import { trigger, state, style, animate, transition } from '@angular/animations';
import { ModalsService } from '../../modals.service';

@Component({
  selector: 'app-daily',
  templateUrl: './daily.component.html',
  styleUrls: ['./daily.component.scss'],
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
export class DailyComponent implements OnInit {
  public tasks: Checklist[] = [];
  public today = new Date();
  public subscription;
  constructor(private plannerService: PlannerService, private modalService: ModalsService) {}

  ngOnInit() {
    this.subscription = this.plannerService.getTodayTasks().subscribe(val => {
      console.log(val);
      this.tasks = val;
      this.tasks.forEach(element => {
        if(!element.state){
          element.state = "active";
        }
      });
    });

    this.plannerService.getTodayTasks();
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
        this.tasks.splice(taskIndex, 1);
      });
    }
    else {
      this.plannerService.updateChecklistItem(item).subscribe(val => {});
    }
  }

  openChecklistModal(task){
    let data = {task: task, planner: 'daily'};
    this.modalService.openChecklistModal(data);
  }

  removeTask(task){
    this.plannerService.deleteChecklist(task).subscribe(val => {
      let index = this.tasks.findIndex(val => val.Id == task.Id);
      this.tasks.splice(index, 1);
    });
  }

}
