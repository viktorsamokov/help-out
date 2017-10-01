import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';

@Injectable()
export class ModalsService {
  agendaModal: Subject<any> = new Subject();

  constructor() { }

  openAgendaModal(data){
    this.agendaModal.next(data);
  }

}
