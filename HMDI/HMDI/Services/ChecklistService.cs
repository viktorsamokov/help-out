﻿using HMDI.Data;
using HMDI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HMDI.Services
{
  public interface IChecklistService
  {
    IEnumerable<Checklist> GetAll();
    Checklist GetById(int id);
    Checklist Create(Checklist checklist);
    Checklist Update(int id, Checklist entity);
    Checklist Delete(int id);
    bool ChecklistExists(int id);
    IEnumerable<Checklist> GetDailyChecklists(string userId);
    IEnumerable<Checklist> GetWeeklyChecklists(string user);
    IEnumerable<Checklist> GetActiveChecklists(string user);
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
      checklist.DateCreated = DateTime.UtcNow;
      checklist.IsFinished = false;

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

    public IEnumerable<Checklist> GetActiveChecklists(string user)
    {
      DateTime startOfToday = DateTime.Today;

      IEnumerable<Checklist> checklists = _db.Checklists.Include(c => c.Items).Where(c => c.UserId == user && 
      (c.DueDate == null || c.DueDate < startOfToday) && c.IsFinished == false).ToList();

      return checklists;
    }

    public IEnumerable<Checklist> GetAll()
    {
      return _db.Checklists.ToList();
    }

    public Checklist GetById(int id)
    {
      return _db.Checklists.Find(id);
    }

    public IEnumerable<Checklist> GetDailyChecklists(string userId)
    {
      DateTime startOfToday = DateTime.Today;
      DateTime endOfToday = DateTime.Today.AddHours(23).AddMinutes(59).AddSeconds(59);

      IEnumerable<Checklist> checklists = _db.Checklists.Include(c =>c.Items).Where(c => c.UserId == userId && 
      (c.DueDate > startOfToday && c.DueDate < endOfToday) && c.IsFinished == false).ToList();

      return checklists;
    }

    public IEnumerable<Checklist> GetWeeklyChecklists(string user)
    {
      DateTime startOfTomorrow = DateTime.Today.AddDays(1);
      DateTime endOfOneWeek = DateTime.Today.AddDays(7).AddHours(23).AddMinutes(59).AddSeconds(59);

      IEnumerable<Checklist> checklists = _db.Checklists.Include(c =>c.Items).Where(c => c.UserId == user && 
      (c.DueDate > startOfTomorrow && c.DueDate < endOfOneWeek) && c.IsFinished == false).ToList();

      return checklists;
    }

    public Checklist Update(int id, Checklist entity)
    {
      Checklist checklist = _db.Checklists.Where(c => c.Id == id).Include(c => c.Items).FirstOrDefault();

      List<ChecklistItem> deletedItems = checklist.Items.Where(i => !entity.Items.Any(i2 => i2.Id == i.Id)).ToList();

      foreach (ChecklistItem item in deletedItems)
      {
        _db.Entry(item).State = EntityState.Deleted;
      }

      if (entity.IsFinished)
      {
        checklist.IsFinished = entity.IsFinished;
        checklist.FinishedAt = DateTime.UtcNow;
        foreach (ChecklistItem item in checklist.Items)
        {
          if(item.IsChecked == false)
          {
            item.IsChecked = true;
            item.CheckedAt = DateTime.UtcNow;
            _db.Entry(item).CurrentValues.SetValues(item);
            _db.Entry(item).State = EntityState.Modified;
          }
        }
      }
      else
      {
        foreach (ChecklistItem item in entity.Items)
        {
          if(item.Id > 0)
          {
            ChecklistItem itemToChange = checklist.Items.Single(i => i.Id == item.Id);
            _db.Entry(itemToChange).CurrentValues.SetValues(item);
            _db.Entry(itemToChange).State = EntityState.Modified;
          }
          else
          {
            checklist.Items.Add(item);
          }
        }
      }

      checklist.DueDate = entity.DueDate;
      checklist.Title = entity.Title;

      _db.Entry(checklist).State = EntityState.Modified;
      _db.SaveChanges();

      return checklist;
    }
  }
}
