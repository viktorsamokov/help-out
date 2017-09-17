using HMDI.Data;
using HMDI.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HMDI.Services
{
  public interface IRatingService
  {
    IEnumerable<Rating> GetAll();
    Rating GetById(int id);
    Rating Create(Rating entity);
    void Update(int id, Rating entity);
    Rating Delete(int id);
    bool RatingExists(int id);
  }

  public class RatingService : IRatingService
  {
    private readonly ApplicationDbContext _db;

    public RatingService(ApplicationDbContext db)
    {
      _db = db;
    }

    public Rating Create(Rating entity)
    {
      _db.Ratings.Add(entity);
      _db.SaveChanges();

      return entity;
    }

    public Rating Delete(int id)
    {
      Rating rating = _db.Ratings.Find(id);

      if(rating == null)
      {
        return null;
      }

      _db.Ratings.Remove(rating);
      _db.SaveChanges();
      
      return rating;
    }

    public IEnumerable<Rating> GetAll()
    {
      return _db.Ratings.ToList();
    }

    public Rating GetById(int id)
    {
      return _db.Ratings.Find(id);
    }

    public bool RatingExists(int id)
    {
      return _db.Ratings.Count(e => e.Id == id) > 0;
    }

    public void Update(int id, Rating entity)
    {
      Rating rating = _db.Ratings.Find(id);

      rating.Five = entity.Five;
      rating.Four = entity.Four;
      rating.Three = entity.Three;
      rating.Two = entity.Two;
      rating.One = entity.One;
      rating.Avg = CalculateAvg(entity);
      rating.TotalVotes = CalculateTotalVotes(entity);

      _db.Entry(rating).State = EntityState.Modified;
      _db.SaveChanges();
    }

    #region Calculations
      private int CalculateTotalVotes(Rating entity)
      {
        return entity.One + entity.Two + entity.Three + entity.Four + entity.Five;
      }

      private double CalculateAvg(Rating entity)
      {
        return (entity.One + entity.Two + entity.Three + entity.Four + entity.Five) / 5; 
      }
    #endregion

  }
}
