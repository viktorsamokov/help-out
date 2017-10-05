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
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using HMDI.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HMDI.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  public class ChecklistsController : Controller
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private IMapper _mapper;
    private IChecklistService _service;

    public ChecklistsController(UserManager<ApplicationUser> userManager, IMapper mapper, IChecklistService service)
    {
      _userManager = userManager;
      _mapper = mapper;
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

    // GET api/checklists/5
    [HttpGet("daily")]
    public IActionResult GetDailyChecklists()
    {
      var user = _userManager.GetUserId(this.User);

      IEnumerable<Checklist> checklists = _service.GetDailyChecklists(user);
      IEnumerable<ChecklistDto> checklistsDto = _mapper.Map<IEnumerable<ChecklistDto>>(checklists);

      if(checklistsDto == null)
      {
        return NotFound();
      }

      return Ok(checklistsDto);
    }

    // GET api/checklists/5
    [HttpGet("weekly")]
    public IActionResult GetWeeklyChecklists()
    {
      var user = _userManager.GetUserId(this.User);

      IEnumerable<Checklist> checklists = _service.GetWeeklyChecklists(user);
      IEnumerable<ChecklistDto> checklistsDto = _mapper.Map<IEnumerable<ChecklistDto>>(checklists);

      if(checklistsDto == null)
      {
        return NotFound();
      }

      return Ok(checklistsDto);
    }

    // GET api/checklists/5
    [HttpGet("active")]
    public IActionResult GetActiveChecklists()
    {
      var user = _userManager.GetUserId(this.User);

      IEnumerable<Checklist> checklists = _service.GetActiveChecklists(user);
      IEnumerable<ChecklistDto> checklistsDto = _mapper.Map<IEnumerable<ChecklistDto>>(checklists);

      if(checklistsDto == null)
      {
        return NotFound();
      }

      return Ok(checklistsDto);
    }
        
    // POST api/checklists
    [HttpPost]
    public IActionResult PostChecklist([FromBody]Checklist entity)
    {
      if (!ModelState.IsValid)
      {
         return BadRequest(ModelState);
      }

      var user = _userManager.GetUserId(this.User);

      entity.UserId = user;

      Checklist checklist = _service.Create(entity);
      ChecklistDto checklistDto = _mapper.Map<ChecklistDto>(checklist);

      return Ok(checklistDto);
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

      Checklist checklist;

      try
      {
        checklist = _service.Update(id, entity);
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
      return Ok(checklist);
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
