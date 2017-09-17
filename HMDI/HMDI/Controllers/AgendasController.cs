using HMDI.Entities;
using HMDI.Helpers;
using HMDI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HMDI.Controllers
{
  [Route("api/[controller]")]
  public class AgendasController : Controller
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private IAgendaService _agendaService;

    public AgendasController(UserManager<ApplicationUser> userManager, IAgendaService agendaSevice)
    {
      _userManager = userManager;
      _agendaService = agendaSevice;
    }

    /// var user = await _userManager.GetUserAsync(HttpContext.User);  

    // GET: api/agendas
    [HttpGet]
    public IActionResult GetAgendas()
    {
      List<Agenda> agendas = _agendaService.GetAll().ToList();
      
      return Ok(agendas);
    }
      
    // GET api/agendas/5
    [HttpGet("{id}")]
    public IActionResult GetAgenda(int id)
    {
      Agenda agenda = _agendaService.GetById(id);
      if(agenda == null)
      {
        return NotFound();
      }

      return Ok(agenda);
    }
        
    // POST api/agendas
    [HttpPost]
    public IActionResult PostAgenda([FromBody]Agenda entity)
    {
      if (!ModelState.IsValid)
      {
         return BadRequest(ModelState);
      }

      Agenda agenda = _agendaService.Create(entity);

      return Ok(agenda);
    }

    // PUT api/agendas/5
    [HttpPut("{id}")]
    public IActionResult PutAgenda(int id, [FromBody]Agenda agenda)
    {
      if (!ModelState.IsValid)
      {
          return BadRequest(ModelState);
      }

      if (id != agenda.Id)
      {
          return BadRequest();
      }

      try
      {
        _agendaService.Update(id, agenda);
      }
      catch(DbUpdateConcurrencyException)
      {
        if (!_agendaService.AgendaExists(id))
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

    // DELETE api/agendas/5
    [HttpDelete("{id}")]
    public IActionResult DeleteAgenda(int id)
    {
      Agenda agenda = _agendaService.Delete(id);

      if(agenda == null)
      {
        return NotFound();
      }

      return Ok(agenda);
    }
  }
}
