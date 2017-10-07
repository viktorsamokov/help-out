using System.ComponentModel.DataAnnotations;

namespace HMDI.Entities
{
  public class FavoriteAgenda
  {
    public int AgendaId { get; set; }
    public Agenda Agenda { get; set; }

    public bool HasRated { get; set; }

    [Range(1, 5)]
    public int Grade { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
  }
}
