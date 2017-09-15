using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMDI.Entities
{
  public class Review
  {
    public int Id { get; set; }

    public string Text { get; set; }

    public DateTime TimeCreated { get; set; }

    [ForeignKey("Agenda")]
    public int AgendaId { get; set; }

    [ForeignKey("User")]
    public string UserID { get; set; }

    public virtual Agenda Agenda { get; set; }

    public virtual ApplicationUser User { get; set; }
  }
}