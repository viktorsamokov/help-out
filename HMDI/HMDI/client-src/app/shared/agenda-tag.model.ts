import { Agenda } from "../user-admin/agendas/category-agendas/category-agenda.model";
import { Tag } from "./tag.model";

export class AgendaTag {
    AgendaId: number;
    Agenda: Agenda;
    TagId: number;
    Tag: Tag;
}