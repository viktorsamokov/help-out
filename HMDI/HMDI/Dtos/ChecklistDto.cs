using System;
using System.Collections.Generic;

namespace HMDI.Dtos
{
  public class ChecklistDto
  {
    public int Id { get; set; }

    public string Title { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DueDate { get; set; }

    public DateTime? FinishedAt { get; set; }

    public bool IsFinished { get; set; }

    public ICollection<ChecklistItemDto> Items { get; set; }
  }
}
