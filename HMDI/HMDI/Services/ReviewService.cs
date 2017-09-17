using HMDI.Data;
using HMDI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMDI.Services
{
  public interface IReviewService
  {
    IEnumerable<Review> GetAll();
    Review GetById(int id);
    Review Create(Review entity);
    void Update(int id, Review entity);
    Review Delete(int id);
    bool ReviewExists(int id);
  }

  public class ReviewService : IReviewService
  {
    private readonly ApplicationDbContext _db;

    public ReviewService(ApplicationDbContext db)
    {
      _db = db;
    }

    public Review Create(Review entity)
    {
      _db.Reviews.Add(entity);
      _db.SaveChanges();

      return entity;
    }

    public Review Delete(int id)
    {
      Review review = _db.Reviews.Find(id);

      if(review == null)
      {
        return null;
      }

      _db.Reviews.Remove(review);
      _db.SaveChanges();
      
      return review;
    }

    public IEnumerable<Review> GetAll()
    {
      return _db.Reviews.ToList();
    }

    public Review GetById(int id)
    {
      return _db.Reviews.Find(id);
    }

    public bool ReviewExists(int id)
    {
      return _db.Reviews.Count(e => e.Id == id) > 0;
    }

    public void Update(int id, Review entity)
    {
      // should not be able to update
      Review review = _db.Reviews.Find(id);
               
      _db.Entry(review).State = EntityState.Modified;
      _db.SaveChanges();
    }
  }
}
