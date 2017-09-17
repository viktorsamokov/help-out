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
  public class ChecklistsController : Controller
  {
    private IChecklistService _service;

    public ChecklistsController(IChecklistService service)
    {
      _service = service;
    }
      
    // GET: api/checklists
    [HttpGet]
    public IActionResult GetChecklists()
    {
      List<Checklist> checklists = _service.GetAll().ToList();
      
      return Ok(checklists);
    }
      
    // GET api/checklists/5
    [HttpGet("{id}")]
    public IActionResult GetChecklist(int id)
    {
      Checklist checklist = _service.GetById(id);
      if(checklist == null)
      {
        return NotFound();
      }

      return Ok(checklist);
    }
        
    // POST api/checklists
    [HttpPost]
    public IActionResult PostChecklist([FromBody]Checklist entity)
    {
      if (!ModelState.IsValid)
      {
         return BadRequest(ModelState);
      }

      Checklist checklist = _service.Create(entity);

      return Ok(checklist);
    }

    // PUT api/checklists/5
    [HttpPut("{id}")]
    public IActionResult PutChecklist(int id, [FromBody]Checklist entity)
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
        if (!_service.ChecklistExists(id))
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

    // DELETE api/checklists/5
    [HttpDelete("{id}")]
    public IActionResult DeleteChecklist(int id)
    {
      Checklist checklist = _service.Delete(id);

      if(checklist == null)
      {
        return NotFound();
      }

      return Ok(checklist);
    }
  }
}
