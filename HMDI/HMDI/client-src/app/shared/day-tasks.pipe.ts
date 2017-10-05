import { Pipe, PipeTransform } from '@angular/core';
import { Checklist } from '../user-admin/planner/checklist.model';

@Pipe({
  name: 'dayTasks',
  pure: false
})
export class DayTasksPipe implements PipeTransform {

  transform(checklists: Checklist[], day: number): Checklist[] {
    if(day == 0){
      return checklists;
    }
    let startOfDay = new Date();
    let endOfDay = new Date();
    startOfDay.setDate(startOfDay.getDate() + day);
    startOfDay.setHours(0, 0);
    endOfDay.setDate(endOfDay.getDate() + day);
    endOfDay.setHours(23, 59);
    return checklists.filter(checklist => {
      return new Date(checklist.DueDate) > startOfDay &&  new Date(checklist.DueDate) < endOfDay;
    });
  }

}
