using HMDI.Data;
using HMDI.Dtos;
using HMDI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Identity;

namespace HMDI.Services
{
  public interface IAgendaCategoryService
  {
    IEnumerable<AgendaCategory> GetAll();
    AgendaCategory GetById(int id);
    AgendaCategory Create(AgendaCategory agenda);
    void Update(int id, AgendaCategory agenda);
    AgendaCategory Delete(int id);
    IEnumerable<AgendaCategoryDto> GetAgendasForUser(string id);
    bool AgendaCategoryExists(int id);
  }

  public class AgendaCategoryService : IAgendaCategoryService
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _db;
    private IHttpContextAccessor _httpContextAccessor;

    public AgendaCategoryService(UserManager<ApplicationUser> userManager, ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
    {
      _userManager = userManager;
      _db = db;
      _httpContextAccessor = httpContextAccessor;
    }

    public bool AgendaCategoryExists(int id)
    {
      return _db.AgendaCategories.Count(e => e.Id == id) > 0;
    }

    public AgendaCategory Create(AgendaCategory agendaCategory)
    {

      _db.AgendaCategories.Add(agendaCategory);
      _db.SaveChanges();

      return agendaCategory;
    }

    public AgendaCategory Delete(int id)
    {
      AgendaCategory agendaCategory = _db.AgendaCategories.Find(id);

      if(agendaCategory == null)
      {
        return null;
      }

      _db.AgendaCategories.Remove(agendaCategory);
      _db.SaveChanges();
      
      return agendaCategory;
    }

    public IEnumerable<AgendaCategoryDto> GetAgendasForUser(string id)
    {
      IEnumerable<AgendaCategoryDto> categories = _db.AgendaCategories.Include(a => a.Agendas).Where(a => a.UserId == id).Select(cat => new AgendaCategoryDto
      {
        AgendasCount = cat.Agendas.Count,
        CategoryName = cat.CategoryName,
        Id = cat.Id
      }).ToList();

      return categories;
    }

    public IEnumerable<AgendaCategory> GetAll()
    {
      //var user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
      return _db.AgendaCategories.ToList();
    }

    public AgendaCategory GetById(int id)
    {
      return _db.AgendaCategories.Find(id);
    }

    public void Update(int id, AgendaCategory entity)
    {
      AgendaCategory agendaCategory = _db.AgendaCategories.Find(entity.Id);

      agendaCategory.CategoryName = entity.CategoryName;

      _db.Entry(agendaCategory).State = EntityState.Modified;
      _db.SaveChanges();
    }
  }
}
