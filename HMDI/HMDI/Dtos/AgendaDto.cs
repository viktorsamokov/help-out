using HMDI.Entities;
using System;
using System.Collections.Generic;

namespace HMDI.Dtos
{
  public class AgendaDto
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDeleted { get; set; }
    public string UserId { get; set; }
    public AgendaStatus Status { get; set; }
    public DateTime DateCreated { get; set; }
    public int AgendaCategoryId { get; set; }
    public LoggedInUser User { get; set; }
    public ICollection<AgendaItemDto> Items { get; set; }
    public ICollection<AgendaTagDto> AgendaTags { get; set; }
  }
}