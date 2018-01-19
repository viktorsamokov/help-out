using HMDI.Data;
using HMDI.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace HMDI.Services
{
  public interface IAgendaService
  {
    IEnumerable<Agenda> GetAll();
    Agenda GetById(int id);
    Agenda Create(Agenda agenda);
    void Update(int id, Agenda agenda);
    Agenda Delete(int id);
    bool AgendaExists(int id);
    IEnumerable<Agenda> GetAgendasForCategory(int id);
    List<Agenda> SearchAgendasByTags(List<Tag> tags, string userId);
    List<Agenda> SearchAgendasByName(string name, string userId);
    FavoriteAgenda SaveToFavorites(Agenda agenda, string userId);
    FavoriteAgenda RateAgenda(FavoriteAgenda favorite, string userId);
    FavoriteAgenda RemoveFavorite(FavoriteAgenda entity, string user);
  }

  public class AgendaService : IAgendaService
  {
    private readonly ApplicationDbContext _db;

    public AgendaService(ApplicationDbContext db)
    {
      _db = db;
    }

    public bool AgendaExists(int id)
    {
      return _db.Agendas.Count(e => e.Id == id) > 0;
    }

    public Agenda Create(Agenda agenda)
    {
      agenda.DateCreated = DateTime.UtcNow;

      List<AgendaTag> tags = agenda.AgendaTags.ToList();

      agenda.AgendaTags = new List<AgendaTag>();

      foreach (AgendaTag tag in tags)
      {
        if (tag.TagId != 0)
        {
          agenda.AgendaTags.Add(new AgendaTag { TagId = tag.TagId, Agenda = agenda });
        }
        else
        {
          agenda.AgendaTags.Add(new AgendaTag { Tag = tag.Tag, Agenda = agenda });
        }
      }

      Rating rating = new Rating() { Agenda = agenda };
      agenda.Rating = rating;

      _db.Agendas.Add(agenda);
      _db.SaveChanges();

      return agenda;
    }

    public Agenda Delete(int id)
    {
      Agenda agenda = _db.Agendas.Find(id);

      if(agenda == null)
      {
        return null;
      }

      agenda.IsDeleted = true;
      agenda.Status = AgendaStatus.Private;

      _db.Agendas.Update(agenda);
      _db.SaveChanges();
      
      return agenda;
    }

    public IEnumerable<Agenda> GetAgendasForCategory(int id)
    {
      IEnumerable<Agenda> agendas = _db.Agendas.Include(a => a.Items).Include(a => a.AgendaTags).ThenInclude(a => a.Tag).Where(a => a.AgendaCategoryId == id && a.IsDeleted == false).ToList();

      return agendas;
    }

    public IEnumerable<Agenda> GetAll()
    {
      return _db.Agendas.ToList();
    }

    public Agenda GetById(int id)
    {
       return _db.Agendas.Find(id);
    }

    public FavoriteAgenda RateAgenda(FavoriteAgenda favorite, string userId)
    {
      Agenda agenda = _db.Agendas.Include(a => a.Favorites).ThenInclude(a => a.Agenda).ThenInclude(a => a.User)
        .Include(u => u.Favorites).ThenInclude(a => a.Agenda).ThenInclude(a => a.Items)
        .Include(a => a.Rating).Where(a => a.Id == favorite.AgendaId).FirstOrDefault();

      favorite.HasRated = true;

      if(agenda == null)
      {
        return null;
      }

      if(favorite.Grade == 1)
      {
        agenda.Rating.One++;
      }
      else if(favorite.Grade == 2)
      {
        agenda.Rating.Two++;
      }
      else if(favorite.Grade == 3)
      {
        agenda.Rating.Three++;
      }
      else if(favorite.Grade == 4)
      {
        agenda.Rating.Four++;
      }
      else if(favorite.Grade == 5)
      {
        agenda.Rating.Five++;
      }

      agenda.Rating.TotalVotes++;
      agenda.Rating.Avg = CalculateAvg(agenda.Rating);

      FavoriteAgenda fav = agenda.Favorites.Single(a => a.UserId == userId && a.AgendaId == agenda.Id);
      _db.Entry(fav).CurrentValues.SetValues(favorite);
      _db.Entry(fav).State = EntityState.Modified;

      _db.Update(agenda);
      _db.SaveChanges();

      return fav;
    }

    public FavoriteAgenda RemoveFavorite(FavoriteAgenda entity, string user)
    {
      Agenda agenda = _db.Agendas.Include(a => a.Favorites).ThenInclude(a => a.Agenda).ThenInclude(a => a.User)
        .Include(u => u.Favorites).ThenInclude(a => a.Agenda).ThenInclude(a => a.Items)
        .Include(a => a.Rating).Where(a => a.Id == entity.AgendaId).FirstOrDefault();

      FavoriteAgenda fav = agenda.Favorites.Single(a => a.UserId == user && a.AgendaId == agenda.Id);
      _db.Entry(fav).State = EntityState.Deleted;

      _db.Update(agenda);
      _db.SaveChanges();

      return entity;
    }

    public FavoriteAgenda SaveToFavorites(Agenda agenda, string userId)
    {
      ApplicationUser user = _db.Users.Include(u => u.Favorites).Where(u => u.Id == userId).FirstOrDefault();

      var exist = user.Favorites.Where(f => f.AgendaId == agenda.Id).FirstOrDefault();

      if (exist != null)
      {
        return null;
      }

      user.Favorites.Add(new FavoriteAgenda
      {
        AgendaId = agenda.Id,
        UserId = userId,
        HasRated = false
      });

      _db.Users.Update(user);
      _db.SaveChanges();

      FavoriteAgenda fav = user.Favorites.Where(f => f.AgendaId == agenda.Id).FirstOrDefault();
      fav.Agenda = agenda;

      return fav;
    }

    public List<Agenda> SearchAgendasByName(string name, string userId)
    {
      string[] words = name.Split(' ');

      IEnumerable<Agenda> favoredAgendas = _db.Agendas.Include(a => a.Favorites).Where(a => a.Favorites.Any(af => af.UserId == userId));

      IEnumerable<Agenda> agendas = _db.Agendas.Include(a => a.Items).Include(a => a.User).Include(a => a.Rating)
        .Where(a => a.UserId != userId && a.IsDeleted == false && a.Status == AgendaStatus.Public && 
        words.All(w => a.Title.ToLower().Contains(w.ToLower()))).Except(favoredAgendas).OrderByDescending(a => a.Rating.Avg).Take(15);

      return agendas.ToList();
    }

    public List<Agenda> SearchAgendasByTags(List<Tag> tags, string userId)
    {
      IEnumerable<Agenda> favoredAgendas = _db.Agendas.Include(a => a.Favorites).Where(a => a.Favorites.Any(af => af.UserId == userId));

      IEnumerable<Agenda> agendas = _db.Agendas.Include(a => a.Items).Include(a => a.User).Include(a => a.Rating).
        Where(a => a.UserId != userId && a.IsDeleted == false && a.Status == AgendaStatus.Public 
        && tags.Any(t => a.AgendaTags.Any(at => at.Tag.Name.ToLower() == t.Name.ToLower())))
        .Except(favoredAgendas)
        .OrderByDescending(a => tags.Count(t => a.AgendaTags.Any(at => at.Tag.Name.ToLower() == t.Name.ToLower())))
        .OrderByDescending(a => a.Rating.Avg).Take(15);

      return agendas.ToList();
    }

    public void Update(int id, Agenda entity)
    {
      Agenda agenda = _db.Agendas.Where(a => a.Id == id).Include(i => i.Items).FirstOrDefault();

      agenda.AgendaCategoryId = entity.AgendaCategoryId;
      agenda.Description = entity.Description;
      agenda.Title = entity.Title;
      agenda.Status = entity.Status;
      agenda.IsDeleted = entity.IsDeleted;
      
      List<AgendaItem> deletedItems = agenda.Items.Where(i => !entity.Items.Any(i2 => i2.Id == i.Id)).ToList();

      foreach (AgendaItem item in deletedItems)
      {
        _db.Entry(item).State = EntityState.Deleted;
      }

      foreach (AgendaItem item in entity.Items)
      {
        if(item.Id > 0)
        {
          AgendaItem itemToChange = agenda.Items.Single(i => i.Id == item.Id);
          _db.Entry(itemToChange).CurrentValues.SetValues(item);
          _db.Entry(itemToChange).State = EntityState.Modified;
        }
        else
        {
          agenda.Items.Add(item);
        }
      }

      _db.Entry(agenda).State = EntityState.Modified;

      _db.SaveChanges();
    }

    #region CalculateAvg
    private double CalculateAvg(Rating rating)
    {
      return (rating.One * 1 + rating.Two * 2 + rating.Three * 3 + rating.Four * 4 + rating.Five * 5) / rating.TotalVotes;
    }
    #endregion
  }
}
