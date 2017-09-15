using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMDI.Entities
{
  public class ChecklistItem
  {
    [Key]
    public int Id { get; set; }

    public string Todo { get; set; }

    public bool IsChecked { get; set; }

    public DateTime? CheckedAt { get; set; }

    [ForeignKey("Checklist")]
    public int ChecklistId { get; set; }

    public virtual Checklist Checklist { get; set; }

  }
}