using HMDI.Entities;
using HMDI.Helpers;
using HMDI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HMDI.Controllers
{
  [Route("api/[controller]")]
  public class AgendaItemsController : Controller
  {
    private IAgendaItemService _service;

    public AgendaItemsController(IAgendaItemService service)
    {
      _service = service;
    }
      
    // GET: api/agendaItems
    [HttpGet]
    public IActionResult GetAgendaItems()
    {
      List<AgendaItem> items = _service.GetAll().ToList();
      
      return Ok(items);
    }
      
    // GET api/agendaItems/5
    [HttpGet("{id}")]
    public IActionResult GetAgendaItem(int id)
    {
      AgendaItem item = _service.GetById(id);
      if(item == null)
      {
        return NotFound();
      }

      return Ok(item);
    }
        
    // POST api/agendaItems
    [HttpPost]
    public IActionResult PostAgendaItem([FromBody]AgendaItem entity)
    {
      if (!ModelState.IsValid)
      {
         return BadRequest(ModelState);
      }

      AgendaItem item = _service.Create(entity);

      return Ok(item);
    }

    // PUT api/agendaItems/5
    [HttpPut("{id}")]
    public IActionResult PutAgendaItem(int id, [FromBody]AgendaItem item)
    {
      if (!ModelState.IsValid)
      {
          return BadRequest(ModelState);
      }

      if (id != item.Id)
      {
          return BadRequest();
      }

      try
      {
        _service.Update(id, item);
      }
      catch(DbUpdateConcurrencyException)
      {
        if (!_service.AgendaItemExists(id))
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

    // DELETE api/agendaItems/5
    [HttpDelete("{id}")]
    public IActionResult DeleteAgendaItem(int id)
    {
      AgendaItem item = _service.Delete(id);

      if(item == null)
      {
        return NotFound();
      }

      return Ok(item);
    }
  }
}
