using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMDI.Entities
{
    public enum AgendaStatus
    {
      Private, Public
    }
      
    public class Agenda
    {
        [Key]
        public int Id { get; set; }
      
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public AgendaStatus Status { get; set; }

        public DateTime DateCreated { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        [ForeignKey("AgendaCategory")]
        public int AgendaCategoryId { get; set; }

        public virtual Rating Rating { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual AgendaCategory AgendaCategory { get; set; }

        public virtual ICollection<Review> Reviews{ get; set; }

        public virtual ICollection<AgendaItem> Items { get; set; }
    }
}
