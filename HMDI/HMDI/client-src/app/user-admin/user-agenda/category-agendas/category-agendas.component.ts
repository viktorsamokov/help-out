import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { UserAgendaCategory } from "../user-agenda-category.model";
import { UserAgendaService } from "../user-agenda.service";
import { Agenda } from "./category-agenda.model";

@Component({
  selector: 'app-category-agendas',
  templateUrl: './category-agendas.component.html',
  styleUrls: ['./category-agendas.component.scss']
})
export class CategoryAgendasComponent implements OnInit {
  categoryAgendas: Agenda[] = [];
  id: number;
  private sub: any;
  constructor(private route: ActivatedRoute, private agendaService: UserAgendaService) { }

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
    
  }

}
