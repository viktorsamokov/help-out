import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'angular-bootstrap-md';

@Component({
  selector: 'app-checklist-modal',
  templateUrl: './checklist-modal.component.html',
  styleUrls: ['./checklist-modal.component.scss']
})
export class ChecklistModalComponent {
  @ViewChild('checklistModal') public checklistModal: ModalDirective;

  constructor() { }

  open(data){
    this.checklistModal.show();
  }

}
