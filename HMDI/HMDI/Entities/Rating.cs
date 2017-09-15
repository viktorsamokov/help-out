using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMDI.Entities
{
  public class Rating
  {
    [Key, ForeignKey("Agenda")]
    public int Id { get; set; }

    public int One { get; set; }

    public int Two { get; set; }

    public int Three { get; set; }

    public int Four { get; set; }

    public int Five { get; set; }

    public double Avg { get; set; }

    public int TotalVotes { get; set; }

    public Agenda Agenda { get; set; }
  }
}