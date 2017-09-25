using AutoMapper;
using HMDI.Dtos;
using HMDI.Entities;
using HMDI.Helpers;
using HMDI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMDI.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  public class AgendaCategoriesController : Controller
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private IAgendaCategoryService _agendaCategoryService;
    private IMapper _mapper;

    public AgendaCategoriesController(UserManager<ApplicationUser> userManager,IMapper mapper, IAgendaCategoryService agendaCategoryService)
    {
      _userManager = userManager;
      _agendaCategoryService = agendaCategoryService;
      _mapper = mapper;
    }

    // GET: api/agendacategories
    [HttpGet]
    public IActionResult GetAgendaCategories()
    {
      List<AgendaCategory> agendaCategories = _agendaCategoryService.GetAll().ToList();
      
      return Ok(agendaCategories);
    }

    // GET: api/agendacategories
    [HttpGet]
    [Route("user")]
    public IActionResult GetUserAgendaCategories()
    {
      var user = _userManager.GetUserId(this.User);

      IEnumerable<AgendaCategoryDto> agendaCategories = _agendaCategoryService.GetAgendasForUser(user).ToList();
      
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

      var user = _userManager.GetUserId(this.User);
      entity.UserId = user;

      AgendaCategory agendaCategory = _agendaCategoryService.Create(entity);
      AgendaCategoryDto agendaCategoryDto = _mapper.Map<AgendaCategoryDto>(agendaCategory);
      agendaCategoryDto.AgendasCount = 0;

      return Ok(agendaCategoryDto);
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
