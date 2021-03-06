import { Component, OnInit, ViewChild } from '@angular/core';
import { AgendaModalComponent } from "./agenda-modal/agenda-modal.component";
import { ModalsService } from "./modals.service";
import { ChecklistModalComponent } from './checklist-modal/checklist-modal.component';

@Component({
  selector: 'app-user-admin',
  templateUrl: './user-admin.component.html',
  styleUrls: ['./user-admin.component.scss']
})
export class UserAdminComponent implements OnInit {
  @ViewChild('agendaModal') public agendaModal: AgendaModalComponent;
  @ViewChild('checklistModal') public checklistModal: ChecklistModalComponent;

  constructor(private modalService: ModalsService) {
    this.modalService.agendaModal.subscribe(data => {
      this.agendaModal.open(data);
    });

    this.modalService.checklistModal.subscribe(data => {
      this.checklistModal.open(data);
    })
  }

  ngOnInit() {
  }

}
