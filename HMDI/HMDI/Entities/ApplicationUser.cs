using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;

namespace HMDI.Entities
{
  // Add profile data for application users by adding properties to the ApplicationUser class
  public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string Avatar { get; set; }

        public virtual ICollection<AgendaCategory> AgendaCategories { get; set; }

        public virtual ICollection<Checklist> Checklists { get; set; }

    }
}
