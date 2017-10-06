using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using HMDI.Entities;
using AutoMapper;
using HMDI.Services;
using Microsoft.EntityFrameworkCore;
using HMDI.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HMDI.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  public class TagsController : Controller
  {

    private readonly UserManager<ApplicationUser> _userManager;
    private ITagService _service;
    private IMapper _mapper;

    public TagsController(UserManager<ApplicationUser> userManager, ITagService service, IMapper mapper)
    {
      _userManager = userManager;
      _service = service;
      _mapper = mapper;
    }
      // GET: api/tags
    [HttpGet]
    public IActionResult GetTags()
    {
      List<Tag> tags = _service.GetAll().ToList();
      
      return Ok(tags);
    }

    // GET api/tags/search
    [HttpGet("search")]
    public IActionResult SearchTags([FromQuery]string term)
    {
      IEnumerable<Tag> tags = _service.SearchTags(term);

      if(tags == null)
      {
        return NotFound();
      }

      return Ok(tags);
    }
      
    // GET api/tags/5
    [HttpGet("{id}")]
    public IActionResult GetTag(int id)
    {
      Tag tag = _service.GetById(id);
      if(tag == null)
      {
        return NotFound();
      }

      return Ok(tag);
    }
        
    // POST api/tags
    [HttpPost]
    public IActionResult PostTag([FromBody]Tag entity)
    {
      if (!ModelState.IsValid)
      {
         return BadRequest(ModelState);
      }

      Tag tag = _service.Create(entity);

      return Ok(tag);
    }

    // PUT api/tags/5
    [HttpPut("{id}")]
    public IActionResult PutTag(int id, [FromBody]Tag entity)
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
        if (!_service.TagExists(id))
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

    // DELETE api/tags/5
    [HttpDelete("{id}")]
    public IActionResult DeleteTag(int id)
    {
      Tag tag = _service.Delete(id);

      if(tag == null)
      {
        return NotFound();
      }

      return Ok(tag);
    }  
  }
}
