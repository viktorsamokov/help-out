import { AgendaItem } from "./agenda-item.model";
import { AgendaTag } from "../../../shared/agenda-tag.model";

export class Agenda {
    Id: number;
    Title: string;
    Description: string;
    IsDeleted: boolean;
    Status: number;
    DateCreated: Date;
    AgendaCategoryId: number;
    Items: Array<AgendaItem>;
    AgendaTags: Array<AgendaTag>;
    state: string;
}