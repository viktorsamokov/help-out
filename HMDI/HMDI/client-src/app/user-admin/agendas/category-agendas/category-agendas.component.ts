import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { AgendaCategory } from "../agenda-category.model";
import { AgendasService } from "../agendas.service";
import { Agenda } from "./category-agenda.model";
import { ModalsService } from "../../modals.service";

@Component({
  selector: 'app-category-agendas',
  templateUrl: './category-agendas.component.html',
  styleUrls: ['./category-agendas.component.scss']
})
export class CategoryAgendasComponent implements OnInit {
  categoryAgendas: Agenda[] = [];
  id: number;
  private sub: any;

  constructor(private modalService: ModalsService, private route: ActivatedRoute,
     private agendaService: AgendasService) { }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      this.id = +params['id']; // (+) converts string 'id' to a number
      console.log(this.id);
      this.agendaService.getAgendasByCategory(this.id).subscribe(agendas => {
        this.categoryAgendas = agendas;
        console.log(this.categoryAgendas);
      })
    });
  }

  openAgendaModal(){
    this.modalService.openAgendaModal(this.id);
  }

}
