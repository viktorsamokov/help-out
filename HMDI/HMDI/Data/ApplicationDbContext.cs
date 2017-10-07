using HMDI.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMDI.Data
{
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<AgendaCategory> AgendaCategories { get; set; }
    public DbSet<Agenda> Agendas { get; set; }
    public DbSet<AgendaItem> AgendaItems { get; set; }
    public DbSet<Checklist> Checklists { get; set; }
    public DbSet<ChecklistItem> ChecklistItems { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
          
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
      // Customize the ASP.NET Identity model and override the defaults if needed.
      // For example, you can rename the ASP.NET Identity table names and more.
      // Add your customizations after calling base.OnModelCreating(builder);

      builder.Entity<AgendaTag>()
        .HasKey(t => new { t.AgendaId, t.TagId });

      builder.Entity<AgendaTag>()
        .HasOne(at => at.Agenda)
        .WithMany(at => at.AgendaTags)
        .HasForeignKey(at => at.AgendaId);

      builder.Entity<AgendaTag>()
        .HasOne(at => at.Tag)
        .WithMany(at => at.AgendaTags)
        .HasForeignKey(at => at.TagId);

      builder.Entity<FavoriteAgenda>()
        .HasKey(f => new { f.AgendaId, f.UserId });

      builder.Entity<FavoriteAgenda>()
        .HasOne(at => at.Agenda)
        .WithMany(at => at.Favorites)
        .HasForeignKey(at => at.AgendaId);

      builder.Entity<FavoriteAgenda>()
        .HasOne(at => at.User)
        .WithMany(at => at.Favorites)
        .HasForeignKey(at => at.UserId);
    }
  }
}
