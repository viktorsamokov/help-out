import { Component, OnInit } from '@angular/core';
import { ModalsService } from '../modals.service';

@Component({
  selector: 'app-planner',
  templateUrl: './planner.component.html',
  styleUrls: ['./planner.component.scss']
})
export class PlannerComponent implements OnInit {

  constructor(private modalService: ModalsService) { }

  ngOnInit() {
  }

  openChecklistModal(){
    let data = {};
    this.modalService.openChecklistModal(data);
  }

}
