using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HMDI.Services;
using HMDI.Entities;
using Microsoft.EntityFrameworkCore;
using HMDI.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HMDI.Controllers
{
  [Route("api/[controller]")]
  public class ReviewsController : Controller
  {
    private IReviewService _service;

    public ReviewsController(IReviewService service)
    {
      _service = service;
    }
      
    // GET: api/reviews
    [HttpGet]
    public IActionResult GetReviews()
    {
      List<Review> review = _service.GetAll().ToList();
      
      return Ok(review);
    }
      
    // GET api/reviews/5
    [HttpGet("{id}")]
    public IActionResult GetReview(int id)
    {
      Review review = _service.GetById(id);
      if(review == null)
      {
        return NotFound();
      }

      return Ok(review);
    }
        
    // POST api/reviews
    [HttpPost]
    public IActionResult PostReview([FromBody]Review entity)
    {
      if (!ModelState.IsValid)
      {
         return BadRequest(ModelState);
      }

      Review review = _service.Create(entity);

      return Ok(review);
    }

    // PUT api/reviews/5
    [HttpPut("{id}")]
    public IActionResult PutReview(int id, [FromBody]Review entity)
    {
      if (!ModelState.IsValid)
      {
          return BadRequest(ModelState);
      }

      if (id != entity.Id)
      {
          return BadRequest();
      }

      try
      {
        _service.Update(id, entity);
      }
      catch(DbUpdateConcurrencyException)
      {
        if (!_service.ReviewExists(id))
        {
            return NotFound();
        }
        else
        {
          throw new AppException("Update failed");
        }
      }
      return NoContent();
    }

    // DELETE api/reviews/5
    [HttpDelete("{id}")]
    public IActionResult DeleteReview(int id)
    {
      Review review = _service.Delete(id);

      if(review == null)
      {
        return NotFound();
      }

      return Ok(review);
    } 
  }
}
