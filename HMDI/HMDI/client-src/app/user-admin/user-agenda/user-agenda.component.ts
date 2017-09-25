import { Component, OnInit } from '@angular/core';
import { UserAgendaService } from "./user-agenda.service";
import { Router, ActivatedRoute } from "@angular/router";
import { UserAgendaCategory } from "./user-agenda-category.model";

@Component({
  selector: 'app-user-agenda',
  templateUrl: './user-agenda.component.html',
  styleUrls: ['./user-agenda.component.scss']
})
export class UserAgendaComponent implements OnInit {
  isCategoryFormOpen: boolean;
  categoryName: string;
  openedCategoryName: string;
  agendaCategories: UserAgendaCategory[] = [];

  constructor(private router: Router, private route: ActivatedRoute, 
    private agendaService: UserAgendaService) { 
      this.isCategoryFormOpen = false;
    }

  ngOnInit() {
    console.log("init");
    this.getAgendas();
  }

  getAgendas(){
    this.agendaService.getUserAgendaCategories().subscribe(agendas => {
      this.agendaCategories = agendas;
    });
  }

  openCategory(category){
    this.openedCategoryName = category.categoryName;
    console.log(this.openedCategoryName);
    this.router.navigate(['./category', category.id], { relativeTo: this.route });
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
      let cat = new UserAgendaCategory();
      cat.CategoryName = this.categoryName;
      this.agendaService.createAgendaCategory(cat).subscribe(category => {
        this.getAgendas();
        this.categoryName = null;
        this.isCategoryFormOpen = false;
      })
    }
  }
}
