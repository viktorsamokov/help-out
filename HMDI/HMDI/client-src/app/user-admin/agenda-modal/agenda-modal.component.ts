import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { ModalDirective } from "angular-bootstrap-md";
import { Agenda } from '../agendas/category-agendas/category-agenda.model';
import { AgendaItem } from '../agendas/category-agendas/agenda-item.model';
import { AgendasService } from '../agendas/agendas.service';
import { Observable } from 'rxjs/Observable';
import { TagService } from '../../shared/tag.service';
import { AgendaTag } from '../../shared/agenda-tag.model';
import { Tag } from '../../shared/tag.model';

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
  public model: any;

  constructor(private agendaService: AgendasService, private tagService: TagService) {
    this.agenda = new Agenda();
   }

  formatMatches = (value: any) => value.Name || '';
  
  search = (text$: Observable<string>) => 
    text$
    .debounceTime(300)
    .distinctUntilChanged().switchMap(term => term.length < 2 ? [] :
      this.tagService.search(term)
          .do(() => {}));

  selectTag($event, input){
    let tag = new AgendaTag();
    if($event.item){
      $event.preventDefault();
      let exist = this.agenda.AgendaTags.find(val => val.Tag.Name === $event.item.Name)
      if (exist) { return ;}
      tag.TagId = $event.item.Id;
      tag.Tag = $event.item;
    }
    else {
      if($event.Id){
        let exist = this.agenda.AgendaTags.find(val => val.Tag.Name === $event.Name)
        if (exist) { return ;}
        tag.Tag = $event;
        tag.TagId = $event.Id;
      }
      else {
        let exist = this.agenda.AgendaTags.find(val => val.Tag.Name === $event)
        if (exist) { return ;}
        tag.Tag = new Tag();
        tag.Tag.Name = $event;
      }
    }

    this.agenda.AgendaTags.push(tag);
    
    input.value = '';
  }

  removeTag(tag){
    console.log(tag);
    let index = this.agenda.AgendaTags.findIndex(val => val.Tag.Name === tag.Tag.Name);
    this.agenda.AgendaTags.splice(index, 1);
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
      this.agenda.AgendaTags = new Array<AgendaTag>();     
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

  isInvalid(){
    if(!this.agenda.Title || this.agenda.Title.length < 2 || this.agenda.AgendaTags.length > 5 || !this.agenda.Items || this.agenda.Items.length < 1){
      return true;
    }
    return false;
  }

  public onHidden():void {
    this.loading = false;
    this.agenda = new Agenda();
  }
}
