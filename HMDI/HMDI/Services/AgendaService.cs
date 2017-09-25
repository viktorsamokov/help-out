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
      IEnumerable<Agenda> agendas = _db.Agendas.Include(a => a.Items).Where(a => a.AgendaCategoryId == id).ToList();

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
      Agenda agenda = _db.Agendas.Find(id);

      agenda.AgendaCategoryId = entity.AgendaCategoryId;
      agenda.Description = entity.Description;
      agenda.Title = entity.Title;
      agenda.Status = entity.Status;
      agenda.IsDeleted = entity.IsDeleted;

      _db.Entry(agenda).State = EntityState.Modified;

      _db.SaveChanges();
    }
  }
}
