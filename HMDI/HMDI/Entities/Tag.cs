using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HMDI.Entities
{
  public class Tag
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public ICollection<AgendaTag> AgendaTags { get; set; }
  }
}
