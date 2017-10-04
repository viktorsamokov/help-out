using HMDI.Entities;
using HMDI.Helpers;
using HMDI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HMDI.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  public class ChecklistItemsController : Controller
  {
        private IChecklistItemService _service;

    public ChecklistItemsController(IChecklistItemService service)
    {
      _service = service;
    }
      
    // GET: api/checklistItems
    [HttpGet]
    public IActionResult GetChecklistItems()
    {
      List<ChecklistItem> items = _service.GetAll().ToList();
      
      return Ok(items);
    }
      
    // GET api/checklistItems/5
    [HttpGet("{id}")]
    public IActionResult GetChecklistItem(int id)
    {
      ChecklistItem item = _service.GetById(id);
      if(item == null)
      {
        return NotFound();
      }

      return Ok(item);
    }
        
    // POST api/checklistItems
    [HttpPost]
    public IActionResult PostChecklistItem([FromBody]ChecklistItem entity)
    {
      if (!ModelState.IsValid)
      {
         return BadRequest(ModelState);
      }

      ChecklistItem item = _service.Create(entity);

      return Ok(item);
    }

    // PUT api/checklistItems/5
    [HttpPut("{id}")]
    public IActionResult PutChecklistItem(int id, [FromBody]ChecklistItem entity)
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
        if (!_service.ChecklistItemExists(id))
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

    // DELETE api/checklistItems/5
    [HttpDelete("{id}")]
    public IActionResult DeleteChecklistItem(int id)
    {
      ChecklistItem item = _service.Delete(id);

      if(item == null)
      {
        return NotFound();
      }

      return Ok(item);
    }
       
  }
}
