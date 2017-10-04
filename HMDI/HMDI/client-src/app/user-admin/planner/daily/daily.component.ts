import { Component, OnInit } from '@angular/core';
import { PlannerService } from '../planner.service';
import { Checklist } from '../checklist.model';
import { trigger, state, style, animate, transition } from '@angular/animations';

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
  public tasks: Checklist[];
  public today = new Date();

  constructor(private plannerService: PlannerService) {
    this.plannerService.getTodayTasks().subscribe(val => {
      console.log(val);
      this.tasks = val;
      this.tasks.forEach(element => {
        if(!element.state){
          element.state = "active";
        }
      });
    });
  }

  ngOnInit() {
  }

  toggleTask(task){
    if(!task.state){
      task.state = "active";
    }else {
      task.state = (task.state === 'inactive' ? 'active' : 'inactive');      
    }
  }

  toggleItem(item, task, taskIndex){
    console.log(item.IsChecked);
    let isLastChecked = task.Items.find(el => el.IsChecked == false);
    console.log(isLastChecked);
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

}
