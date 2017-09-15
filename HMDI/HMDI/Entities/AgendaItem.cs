using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMDI.Entities
{
  public class AgendaItem
  {
    [Key]
    public int Id { get; set; }

    public string Todo { get; set; }

    [ForeignKey("Agenda")]
    public int AgendaId { get; set; }

    public virtual Agenda Agenda { get; set; }
  }
}