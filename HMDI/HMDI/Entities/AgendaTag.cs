using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMDI.Entities
{
  public class AgendaTag
  {
    public int AgendaId { get; set; }
    public Agenda Agenda { get; set; }

    public int TagId { get; set; }
    public Tag Tag { get; set; }
  }
}
