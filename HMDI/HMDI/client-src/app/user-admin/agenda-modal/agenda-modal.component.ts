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
  public loading = false;

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
    if(data.agenda){
      this.agenda = JSON.parse(JSON.stringify(data.agenda));
    }
    else {
      this.agenda.AgendaCategoryId = data.id;      
    }
    this.agendaModal.show();
  }

  save(){
    this.loading = true;
    if(this.agenda.Id){
      this.agendaService.updateAgenda(this.agenda).subscribe(val => {
        this.agendaModal.hide();
      })
    }
    else {
      this.agendaService.createAgenda(this.agenda).subscribe(val => {
        this.agendaModal.hide();
      });
    }
  }

  public onHidden():void {
    this.loading = false;
    this.agenda = new Agenda();
  }
}
