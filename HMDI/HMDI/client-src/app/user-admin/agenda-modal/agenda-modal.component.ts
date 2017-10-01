import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { ModalDirective } from "angular-bootstrap-md";
import { Agenda } from '../agendas/category-agendas/category-agenda.model';
import { AgendaItem } from '../agendas/category-agendas/agenda-item.model';
import { AgendasService } from '../agendas/agendas.service';

@Component({
  selector: 'app-agenda-modal',
  templateUrl: './agenda-modal.component.html',
  styleUrls: ['./agenda-modal.component.scss']
})
export class AgendaModalComponent {
  
  @ViewChild('agendaModal') public agendaModal: ModalDirective;
  public agenda: Agenda;
  public agendaItem: string;

  constructor(private agendaService: AgendasService) {
    this.agenda = new Agenda();
   }

  addAgendaItem(agendaItem){
    if(!agendaItem || agendaItem.length < 1){
      return;
    }

    if(!this.agenda.Items){
      this.agenda.Items = new Array<AgendaItem>();
    }

    let item = new AgendaItem();
    item.Todo = agendaItem;

    this.agenda.Items.push(item);
    this.agendaItem = null;
  }

  removeAgendaItem(index){
    this.agenda.Items.splice(index, 1);
  }

  open(data){
    console.log(data);
    this.agendaModal.show();
    this.agenda.AgendaCategoryId = data;
  }

  save(){
    console.log(this.agenda);
    this.agendaService.createAgenda(this.agenda).subscribe(val => {
      console.log(val);
    });
  }

  public onHidden():void {
    console.log("destroy");
    this.agenda = new Agenda();
  }
  
}
