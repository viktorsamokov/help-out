import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';

@Injectable()
export class ModalsService {
  agendaModal: Subject<any> = new Subject();
  checklistModal: Subject<any> = new Subject();

  constructor() { }

  openAgendaModal(data){
    this.agendaModal.next(data);
  }

  openChecklistModal(data){
    this.checklistModal.next(data);
  }

}
