namespace HMDI.Dtos
{
  public class FavoriteAgendaDto
  {
    public int AgendaId { get; set; }
    public AgendaDto Agenda { get; set; }
    public bool HasRated { get; set; }
    public int Grade { get; set; }
    public string UserId { get; set; }
    public LoggedInUser User { get; set; }
  }
}
