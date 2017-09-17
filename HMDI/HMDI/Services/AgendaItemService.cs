using HMDI.Data;
using HMDI.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HMDI.Services
{
  public interface IAgendaItemService
  {
    IEnumerable<AgendaItem> GetAll();
    AgendaItem GetById(int id);
    AgendaItem Create(AgendaItem item);
    void Update(int id, AgendaItem entity);
    AgendaItem Delete(int id);
    bool AgendaItemExists(int id);
  }

  public class AgendaItemService : IAgendaItemService
  {
    private readonly ApplicationDbContext _db;

    public AgendaItemService(ApplicationDbContext db)
    {
      _db = db;
    }

    public bool AgendaItemExists(int id)
    {
      return _db.AgendaItems.Count(e => e.Id == id) > 0;
    }

    public AgendaItem Create(AgendaItem item)
    {
      _db.AgendaItems.Add(item);
      _db.SaveChanges();

      return item;
    }

    public AgendaItem Delete(int id)
    {
       AgendaItem item = _db.AgendaItems.Find(id);

      if(item == null)
      {
        return null;
      }

      _db.AgendaItems.Remove(item);
      _db.SaveChanges();
      
      return item;
    }

    public IEnumerable<AgendaItem> GetAll()
    {
      return _db.AgendaItems.ToList();
    }

    public AgendaItem GetById(int id)
    {
       return _db.AgendaItems.Find(id);
    }

    public void Update(int id, AgendaItem entity)
    {
      AgendaItem item = _db.AgendaItems.Find(id);

      item.Todo = entity.Todo;

      _db.Entry(item).State = EntityState.Modified;
      _db.SaveChanges();
    }
  }
}
