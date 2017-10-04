using HMDI.Data;
using HMDI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HMDI.Services
{
  public interface IChecklistItemService
  {
    IEnumerable<ChecklistItem> GetAll();
    ChecklistItem GetById(int id);
    ChecklistItem Create(ChecklistItem entity);
    void Update(int id, ChecklistItem entity);
    ChecklistItem Delete(int id);
    bool ChecklistItemExists(int id);
  }

  public class ChecklistItemService : IChecklistItemService
  {
    private readonly ApplicationDbContext _db;

    public ChecklistItemService(ApplicationDbContext db)
    {
      _db = db;
    }

    public bool ChecklistItemExists(int id)
    {
      return _db.ChecklistItems.Count(e => e.Id == id) > 0;
    }

    public ChecklistItem Create(ChecklistItem entity)
    {
      _db.ChecklistItems.Add(entity);
      _db.SaveChanges();

      return entity;
    }

    public ChecklistItem Delete(int id)
    {
      ChecklistItem checklistItem = _db.ChecklistItems.Find(id);

      if(checklistItem == null)
      {
        return null;
      }

      _db.ChecklistItems.Remove(checklistItem);
      _db.SaveChanges();
      
      return checklistItem;
    }

    public IEnumerable<ChecklistItem> GetAll()
    {
      return _db.ChecklistItems.ToList();
    }

    public ChecklistItem GetById(int id)
    {
      return _db.ChecklistItems.Find(id);
    }

    public void Update(int id, ChecklistItem entity)
    {
      ChecklistItem item = _db.ChecklistItems.Find(id);

      if(entity.IsChecked == false)
      {
        item.CheckedAt = null;
      }
      else
      {
        item.CheckedAt = DateTime.UtcNow;
      }
      
      item.Todo = entity.Todo;
      item.IsChecked = entity.IsChecked;

      _db.Entry(item).State = EntityState.Modified;
      _db.SaveChanges();
    }
  }
}
