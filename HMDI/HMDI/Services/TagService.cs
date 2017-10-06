using HMDI.Data;
using HMDI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMDI.Services
{
  public interface ITagService
  {
    IEnumerable<Tag> GetAll();
    Tag GetById(int id);
    Tag Create(Tag entity);
    void Update(int id, Tag entity);
    Tag Delete(int id);
    bool TagExists(int id);
    IEnumerable<Tag> SearchTags(string term);
  }

  public class TagService : ITagService
  {
    private readonly ApplicationDbContext _db;

    public TagService(ApplicationDbContext db)
    {
      _db = db;
    }

    public Tag Create(Tag entity)
    {
      _db.Tags.Add(entity);
      _db.SaveChanges();

      return entity;
    }

    public Tag Delete(int id)
    {
      Tag tag = _db.Tags.Find(id);

      if(tag == null)
      {
        return null;
      }

      _db.Tags.Remove(tag);
      _db.SaveChanges();
      
      return tag;
    }

    public IEnumerable<Tag> GetAll()
    {
      return _db.Tags.ToList();
    }

    public Tag GetById(int id)
    {
       return _db.Tags.Find(id);
    }

    public IEnumerable<Tag> SearchTags(string term)
    {
      IEnumerable<Tag> tags = _db.Tags.Where(t => t.Name.ToLower().Contains(term.ToLower())).OrderBy(t => t.Id).Take(10).ToList();
      
      return tags;
    }

    public bool TagExists(int id)
    {
      return _db.Tags.Count(e => e.Id == id) > 0;
    }

    public void Update(int id, Tag entity)
    {
      Tag tag = _db.Tags.Find(id);

      tag.Name = entity.Name;

      _db.Entry(tag).State = EntityState.Modified;
      _db.SaveChanges();
    }
  }
}
