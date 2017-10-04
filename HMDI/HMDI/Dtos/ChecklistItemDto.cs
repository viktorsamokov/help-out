using System;

namespace HMDI.Dtos
{
  public class ChecklistItemDto
  {
    public int Id { get; set; }

    public string Todo { get; set; }

    public bool IsChecked { get; set; }

    public DateTime? CheckedAt { get; set; }

    public int ChecklistId { get; set; }
  }
}
