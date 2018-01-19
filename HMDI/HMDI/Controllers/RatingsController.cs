using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HMDI.Services;
using HMDI.Entities;
using Microsoft.EntityFrameworkCore;
using HMDI.Helpers;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HMDI.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  public class RatingsController : Controller
  {
    private IRatingService _service;

    public RatingsController(IRatingService service)
    {
      _service = service;
    }
      
    // GET: api/ratings
    [HttpGet]
    public IActionResult GetRatings()
    {
      List<Rating> ratings = _service.GetAll().ToList();
      
      return Ok(ratings);
    }
      
    // GET api/ratings/5
    [HttpGet("{id}")]
    public IActionResult GetRating(int id)
    {
      Rating rating = _service.GetById(id);
      if(rating == null)
      {
        return NotFound();
      }

      return Ok(rating);
    }
        
    // POST api/ratings
    [HttpPost]
    public IActionResult PostRating([FromBody]Rating entity)
    {
      if (!ModelState.IsValid)
      {
         return BadRequest(ModelState);
      }

      Rating rating = _service.Create(entity);

      return Ok(rating);
    }

    // PUT api/ratings/5
    [HttpPut("{id}")]
    public IActionResult PutRating(int id, [FromBody]Rating entity)
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
        if (!_service.RatingExists(id))
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

    // DELETE api/ratings/5
    [HttpDelete("{id}")]
    public IActionResult DeleteRating(int id)
    {
      Rating rating = _service.Delete(id);

      if(rating == null)
      {
        return NotFound();
      }

      return Ok(rating);
    }  
  }
}
