﻿using AutoMapper;
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

namespace HMDI.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  public class AgendasController : Controller
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private IAgendaService _agendaService;
    private IMapper _mapper;

    public AgendasController(UserManager<ApplicationUser> userManager, IAgendaService agendaSevice, IMapper mapper)
    {
      _userManager = userManager;
      _agendaService = agendaSevice;
      _mapper = mapper;
    }

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

    // GET api/agendas/category/5
    [HttpGet("category")]
    public IActionResult GetAgendasForCategory([FromQuery]int id)
    {
      IEnumerable<Agenda> agendas = _agendaService.GetAgendasForCategory(id);

      IEnumerable<AgendaDto> agendasDto = _mapper.Map<IEnumerable<AgendaDto>>(agendas);

      if(agendasDto == null)
      {
        return NotFound();
      }

      return Ok(agendasDto);
    }
        
    // POST api/agendas
    [HttpPost]
    public IActionResult PostAgenda([FromBody]Agenda entity)
    {
      if (!ModelState.IsValid)
      {
         return BadRequest(ModelState);
      }

      var user = _userManager.GetUserId(this.User);

      entity.UserId = user;

      Agenda agenda = _agendaService.Create(entity);
      AgendaDto agendaDto = _mapper.Map<AgendaDto>(agenda);

      return Ok(agendaDto);
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
