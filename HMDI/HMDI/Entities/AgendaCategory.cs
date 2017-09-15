using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMDI.Entities
{
  public class AgendaCategory
  {
    [Key]
    public int Id { get; set; }

    public string CategoryName { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }

    public virtual ApplicationUser User { get; set; }

    public virtual ICollection<Agenda> Agendas { get; set; }
  }
}