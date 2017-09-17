using HMDI.Data;
using HMDI.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HMDI.Services
{
  public interface IChecklistService
  {
    IEnumerable<Checklist> GetAll();
    Checklist GetById(int id);
    Checklist Create(Checklist checklist);
    void Update(int id, Checklist entity);
    Checklist Delete(int id);
    bool ChecklistExists(int id);
  }

  public class ChecklistService : IChecklistService
  {
    private readonly ApplicationDbContext _db;

    public ChecklistService(ApplicationDbContext db)
    {
      _db = db;
    }

    public bool ChecklistExists(int id)
    {
      return _db.Checklists.Count(e => e.Id == id) > 0;
    }

    public Checklist Create(Checklist checklist)
    {
      _db.Checklists.Add(checklist);
      _db.SaveChanges();

      return checklist;
    }

    public Checklist Delete(int id)
    {
      Checklist checklist = _db.Checklists.Find(id);

      if(checklist == null)
      {
        return null;
      }

      _db.Checklists.Remove(checklist);
      _db.SaveChanges();
      
      return checklist;
    }

    public IEnumerable<Checklist> GetAll()
    {
      return _db.Checklists.ToList();
    }

    public Checklist GetById(int id)
    {
      return _db.Checklists.Find(id);
    }

    public void Update(int id, Checklist entity)
    {
      Checklist checklist = _db.Checklists.Find(id);

      checklist.DueDate = entity.DueDate;
      checklist.FinishedAt = entity.FinishedAt;
      checklist.IsFinished = entity.IsFinished;
      checklist.Title = entity.Title;

      _db.Entry(checklist).State = EntityState.Modified;
      _db.SaveChanges();
    }
  }
}
