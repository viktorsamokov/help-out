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

      _db.Agendas.Remove(agenda);
      _db.SaveChanges();
      
      return agenda;
    }

    public IEnumerable<Agenda> GetAgendasForCategory(int id)
    {
      IEnumerable<Agenda> agendas = _db.Agendas.Include(a => a.Items).Include(a => a.AgendaTags).ThenInclude(a => a.Tag).Where(a => a.AgendaCategoryId == id).ToList();

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
  }
}
