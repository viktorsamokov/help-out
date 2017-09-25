using HMDI.Entities;
using System;

namespace HMDI.Dtos
{
  public class AgendaDto
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDeleted { get; set; }
    public AgendaStatus Status { get; set; }
    public DateTime DateCreated { get; set; }
    public int AgendaCategoryId { get; set; }
    public AgendaItemDto Items { get; set; }
  }
}