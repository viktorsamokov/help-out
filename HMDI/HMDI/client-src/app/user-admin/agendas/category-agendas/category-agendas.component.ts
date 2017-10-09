import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { AgendaCategory } from "../agenda-category.model";
import { AgendasService } from "../agendas.service";
import { Agenda } from "./category-agenda.model";
import { ModalsService } from "../../modals.service";
import { trigger, state, style, animate, transition } from '@angular/animations';
import { Checklist } from '../../planner/checklist.model';
import { ChecklistItem } from '../../planner/checklist-item.model';

@Component({
  selector: 'app-category-agendas',
  templateUrl: './category-agendas.component.html',
  styleUrls: ['./category-agendas.component.scss'],
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
        transition('inactive => active', animate('300ms ease-in')),
        transition('active => inactive', animate('300ms ease-out'))
    ])
] 
})
export class CategoryAgendasComponent implements OnInit {
  categoryAgendas: Agenda[] = [];
  id: number;
  private sub: any;

  constructor (private modalService: ModalsService, private route: ActivatedRoute,
     private agendaService: AgendasService) { }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      this.id = +params['id']; // (+) converts string 'id' to a number
      this.agendaService.getAgendasByCategory(this.id).subscribe(agendas => {
        this.categoryAgendas = agendas;
        console.log(agendas);
        this.categoryAgendas.forEach(element => {
          element.state = "inactive";
        });
        console.log(this.categoryAgendas);
      })
    });
  }

  toggleAgenda(agenda){
    if(!agenda.state){
      agenda.state = "active";
    }else {
      agenda.state = (agenda.state === 'inactive' ? 'active' : 'inactive');      
    }
  }
  
  convertToChecklist(agenda){
    let checklist = new Checklist();
    checklist.state = "inactive";
    checklist.Title = agenda.Title;
    checklist.IsFinished = false;
    checklist.Items = new Array<ChecklistItem>();
    agenda.Items.forEach(element => {
      let checklistItem = new ChecklistItem();
      checklistItem.IsChecked = false;
      checklistItem.Todo = element.Todo;
      checklist.Items.push(checklistItem);
    });

    let data = { task: checklist, planner: '' };
    this.modalService.openChecklistModal(data);
  }

  openAgendaModal(agenda){
    let data = {
      agenda: agenda,
      id: this.id
    }

    this.modalService.openAgendaModal(data);
  }

  removeAgenda(agenda){
    this.agendaService.removeAgenda(agenda).subscribe(val => {
      
    })
  }

}
