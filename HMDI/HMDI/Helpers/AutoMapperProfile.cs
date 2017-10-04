using AutoMapper;
using HMDI.Dtos;
using HMDI.Entities;

namespace HMDI.Helpers
{
  public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
          CreateMap<ApplicationUser, LoggedInUser>();
          CreateMap<AgendaCategory, AgendaCategoryDto>();
          CreateMap<Agenda, AgendaDto>();
          CreateMap<AgendaItem, AgendaItemDto>();
          CreateMap<Checklist, ChecklistDto>();
          CreateMap<ChecklistItem, ChecklistItemDto>();
        }
    }
}
