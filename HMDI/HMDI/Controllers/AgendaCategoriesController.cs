using HMDI.Entities;
using HMDI.Helpers;
using HMDI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HMDI.Controllers
{
  [Route("api/[controller]")]
  public class AgendaCategoriesController : Controller
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private IAgendaCategoryService _agendaCategoryService;

    public AgendaCategoriesController(UserManager<ApplicationUser> userManager, IAgendaCategoryService agendaCategoryService)
    {
      _userManager = userManager;
      _agendaCategoryService = agendaCategoryService;
    }

    /// var user = await _userManager.GetUserAsync(HttpContext.User);  

    // GET: api/agendacategories
    [HttpGet]
    public IActionResult GetAgendaCategories()
    {
      List<AgendaCategory> agendaCategories = _agendaCategoryService.GetAll().ToList();
      
      return Ok(agendaCategories);
    }
      
    // GET api/agendacategories/5
    [HttpGet("{id}")]
    public IActionResult GetAgendaCategory(int id)
    {
      AgendaCategory agendaCategory = _agendaCategoryService.GetById(id);
      if(agendaCategory == null)
      {
        return NotFound();
      }

      return Ok(agendaCategory);
    }
        
    // POST api/agendacategories
    [HttpPost]
    public IActionResult PostAgendaCategory([FromBody]AgendaCategory entity)
    {
      if (!ModelState.IsValid)
      {
         return BadRequest(ModelState);
      }

      AgendaCategory agendaCategory = _agendaCategoryService.Create(entity);

      return Ok(agendaCategory);
    }

    // PUT api/agendacategories/5
    [HttpPut("{id}")]
    public IActionResult PutAgendaCategory(int id, [FromBody]AgendaCategory agendaCategory)
    {
      if (!ModelState.IsValid)
      {
          return BadRequest(ModelState);
      }

      if (id != agendaCategory.Id)
      {
          return BadRequest();
      }

      try
      {
        _agendaCategoryService.Update(id, agendaCategory);
      }
      catch(DbUpdateConcurrencyException)
      {
        if (!_agendaCategoryService.AgendaCategoryExists(id))
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

    // DELETE api/agendacategories/5
    [HttpDelete("{id}")]
    public IActionResult DeleteAgendaCategory(int id)
    {
      AgendaCategory agendaCategory= _agendaCategoryService.Delete(id);

      if(agendaCategory == null)
      {
        return NotFound();
      }

      return Ok(agendaCategory);
    }
  }
}
