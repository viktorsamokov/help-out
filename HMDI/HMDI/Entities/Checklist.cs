using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMDI.Entities
{
  public class Checklist
  {
    [Key]
    public int Id { get; set; }

    public string Title { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DueDate { get; set; }

    public DateTime? FinishedAt { get; set; }

    public bool IsFinished { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }

    public virtual ApplicationUser User { get; set; }

    public virtual ICollection<ChecklistItem> Items { get; set; }
  }
}