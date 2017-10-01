import { Component, OnInit } from '@angular/core';
import { AgendasService } from "./agendas.service";
import { Router, ActivatedRoute } from "@angular/router";
import { AgendaCategory } from "./agenda-category.model";

@Component({
  selector: 'app-user-agenda',
  templateUrl: './agendas.component.html',
  styleUrls: ['./agendas.component.scss']
})
export class AgendasComponent implements OnInit {
  isCategoryFormOpen: boolean;
  categoryName: string;
  openedCategoryName: string;
  agendaCategories: AgendaCategory[] = [];

  constructor(private router: Router, private route: ActivatedRoute, 
    private agendaService: AgendasService) { 
      this.isCategoryFormOpen = false;
    }

  ngOnInit() {
    this.getAgendas();
  }

  getAgendas(){
    this.agendaService.getUserAgendaCategories().subscribe(agendas => {
      this.agendaCategories = agendas;
    });
  }

  openCategory(category){
    console.log("go");
    this.openedCategoryName = category.CategoryName;
    this.router.navigate(['./category', category.Id], { relativeTo: this.route });
  }

  openCategoryForm(){
    this.isCategoryFormOpen = true;
  }

  closeCategoryForm(){
    this.isCategoryFormOpen = false;
    this.categoryName = "";
  }

  saveCategory(){
    if(this.categoryName && this.categoryName.length > 0){
      let cat = new AgendaCategory();
      cat.CategoryName = this.categoryName;
      this.agendaService.createAgendaCategory(cat).subscribe(category => {
        this.getAgendas();
        this.categoryName = null;
        this.isCategoryFormOpen = false;
      })
    }
  }

  deleteCategory(){
    console.log("delete category");
  }
}
