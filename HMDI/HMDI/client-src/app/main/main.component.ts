import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { MainService } from './main.service';
import { AgendaTag } from '../shared/agenda-tag.model';
import { Tag } from '../shared/tag.model';
import { Observable } from 'rxjs/Observable';
import { TagService } from '../shared/tag.service';
import { Agenda } from '../user-admin/agendas/category-agendas/category-agenda.model';
import { trigger, state, style, animate, transition } from '@angular/animations';
import { FavoritesService } from '../user-admin/favorites/favorites.service';
import { ToastsManager } from 'ng2-toastr';

@Component({
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
  animations: [
    trigger('animateHeight', [
        state('inactive', style({
          height: '0',
          opacity: 0
        })),
        state('active', style({
          height: '*',
          opacity: 1
        })),
        transition('inactive => active', animate('300ms ease-in')),
        transition('active => inactive', animate('300ms ease-out'))
    ])
  ] 
})
export class MainComponent implements OnInit {
  public searchByTag: boolean = true;
  public agendaTitle: any;
  public tags: Array<Tag> = [];
  public agendas: Array<Agenda>;
  public model: any;
  public loading = false;
  formatMatches = (value: any) => value.Name || '';

  constructor(private mainService: MainService, private favoriteService: FavoritesService, 
    private tagService: TagService,  public toastr: ToastsManager, vcr: ViewContainerRef) {
      this.toastr.setRootViewContainerRef(vcr);      
     }

  ngOnInit() {
  }

  search = (text$: Observable<string>) => 
  text$
  .debounceTime(300)
  .distinctUntilChanged().switchMap(term => term.length < 2 ? [] :
    this.tagService.search(term)
        .do(() => {}));

  selectTag($event, input){
    let exist = this.tags.find(val => val.Name === $event.item.Name)
    if (exist) { return ;}

    if($event.item){
      $event.preventDefault();
      this.tags.push($event.item)
    }
    else {
      this.tags.push($event);
    }

    input.value = '';
  }

  removeTag(tag, index){
    console.log(index);
    this.tags.splice(index, 1);
  }

  searchByTags(){
    this.loading = true;
    this.mainService.searchByTags(this.tags).subscribe(agendas => {
      if(!this.agendas){
        this.agendas = new Array<Agenda>();
      }
      agendas.forEach(val => {
        val.state = "inactive";
      });
      console.log(agendas);
      this.loading = false;
      this.agendas = agendas;
    })
  }

  toggleAgenda(agenda){
    if(!agenda.state){
      agenda.state = "active";
    }else {
      agenda.state = (agenda.state === 'inactive' ? 'active' : 'inactive');      
    }
  }

  searchByName(agendaTitle){
    if(agendaTitle.length < 1){
      return;
    }
    this.loading = true;
    this.mainService.searchByName(agendaTitle).subscribe(agendas => {
      if(!this.agendas){
        this.agendas = new Array<Agenda>();
      }
      agendas.forEach(val => {
        val.state = "inactive";
      });
      this.loading = false;
      this.agendas = agendas;
    })
  }

  saveToFavorites(agenda){
    this.loading = true;
    this.mainService.addToFavorites(agenda).subscribe(val => {
      this.favoriteService.addToFavorites(val);
      this.loading = false;
      this.toastr.success("You have added " + agenda.Title + " to favorites", "Success");      
      for (var index = 0; index < this.agendas.length; index++) {
        var element = this.agendas[index];
        if(element.Id == agenda.Id){
          this.agendas.splice(index, 1);
        }
      }
    });
  }
}
