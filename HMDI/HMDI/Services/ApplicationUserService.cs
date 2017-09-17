using HMDI.Data;
using HMDI.Dtos;
using HMDI.Entities;
using HMDI.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMDI.Services
{
  public interface IApplicationUserService
  {
    ApplicationUser GetById(string id);
    Task<ApplicationUser> Create(RegisterDto registerDto);
    void Update(string id, ApplicationUser entity);
    ApplicationUser Delete(string id);
    bool ApplicationUserExists(string id);
  }

  public class ApplicationUserService : IApplicationUserService
  {
    private readonly ApplicationDbContext _db;
    private UserManager<ApplicationUser> _userManager;
    private IUrlHelper _urlHelper;

    public ApplicationUserService(ApplicationDbContext db, UserManager<ApplicationUser> userManager, IUrlHelper urlHelper)
    {
      _db = db;
      _userManager = userManager;
      _urlHelper = urlHelper;
    }

    public bool ApplicationUserExists(string id)
    {
      return _db.Users.Count(e => e.Id.Equals(id, StringComparison.Ordinal)) > 0;
    }

    public async Task<ApplicationUser> Create(RegisterDto registerDto)
    {
      if (string.IsNullOrWhiteSpace(registerDto.Password))
      {
        throw new AppException("password is required");
      }

      if(_db.Users.Any(x => x.Email == registerDto.Email))
      {
        throw new AppException("Username " + registerDto.Email + " is already taken");
      }

      ApplicationUser newUser = new ApplicationUser()
      {
        Email = registerDto.Email,
        UserName = registerDto.Email,
        FirstName = registerDto.FirstName,
        LastName = registerDto.LastName,
        Avatar = "avatar",
        EmailConfirmed = true,
        AgendaCategories = new List<AgendaCategory>(),
        Checklists = new List<Checklist>()
      };
      
      var result = await _userManager.CreateAsync(newUser, registerDto.Password);

      if (result.Succeeded)
      {
        return newUser;
      }
      else
      {
        throw new AppException("Failed to create user");
      }
    }

    public ApplicationUser Delete(string id)
    {
      throw new NotImplementedException();
    }

    public ApplicationUser GetById(string id)
    {
      return _db.Users.Include(u => u.AgendaCategories).Where(u => u.Id == id).FirstOrDefault();
    }

    public void Update(string id, ApplicationUser entity)
    {
      ApplicationUser user = _db.Users.Find(id);

      user.FirstName = entity.FirstName;
      user.LastName = entity.LastName;

      _db.Entry(user).State = EntityState.Modified;
      _db.SaveChanges();
    }
  }
}
