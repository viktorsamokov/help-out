using HMDI.Data;
using HMDI.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HMDI.Services
{
  public interface IAgendaCategoryService
  {
    IEnumerable<AgendaCategory> GetAll();
    AgendaCategory GetById(int id);
    AgendaCategory Create(AgendaCategory agenda);
    void Update(int id, AgendaCategory agenda);
    AgendaCategory Delete(int id);
    bool AgendaCategoryExists(int id);
  }

  public class AgendaCategoryService : IAgendaCategoryService
  {
    private readonly ApplicationDbContext _db;

    public AgendaCategoryService(ApplicationDbContext db)
    {
      _db = db;
    }

    public bool AgendaCategoryExists(int id)
    {
      return _db.AgendaCategories.Count(e => e.Id == id) > 0;
    }

    public AgendaCategory Create(AgendaCategory agendaCategory)
    {
      _db.AgendaCategories.Add(agendaCategory);
      _db.SaveChanges();

      return agendaCategory;
    }

    public AgendaCategory Delete(int id)
    {
      AgendaCategory agendaCategory = _db.AgendaCategories.Find(id);

      if(agendaCategory == null)
      {
        return null;
      }

      _db.AgendaCategories.Remove(agendaCategory);
      _db.SaveChanges();
      
      return agendaCategory;
    }

    public IEnumerable<AgendaCategory> GetAll()
    {
      return _db.AgendaCategories.ToList();
    }

    public AgendaCategory GetById(int id)
    {
      return _db.AgendaCategories.Find(id);
    }

    public void Update(int id, AgendaCategory entity)
    {
      AgendaCategory agendaCategory = _db.AgendaCategories.Find(entity.Id);

      agendaCategory.CategoryName = entity.CategoryName;

      _db.Entry(agendaCategory).State = EntityState.Modified;
      _db.SaveChanges();
    }
  }
}
