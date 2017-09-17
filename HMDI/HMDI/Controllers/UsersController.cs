using HMDI.Dtos;
using HMDI.Entities;
using HMDI.Helpers;
using HMDI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HMDI.Controllers
{
  [Route("api/[controller]")]
  public class UsersController : Controller
  {
    private IApplicationUserService _userService;

    public UsersController(IApplicationUserService userService)
    {
      _userService = userService;
    }


    // GET: api/users
    [HttpGet]
    public IActionResult GetUsers()
    {
      return NotFound();
    }
      
    // GET api/users/5
    [HttpGet("{id}")]
    public IActionResult GetUser(string id)
    {
      ApplicationUser user = _userService.GetById(id);

      if(user == null)
      {
        return NotFound();
      }

      return Ok(user);
    }
        
    // POST api/users
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> PostUser([FromBody]RegisterDto entity)
    {
      if (!ModelState.IsValid)
      {
         return BadRequest(ModelState);
      }

      ApplicationUser user = await _userService.Create(entity);

      return Ok(user);
    }

    // PUT api/users/5
    [HttpPut("{id}")]
    public IActionResult PutUser(string id, [FromBody]ApplicationUser entity)
    {
      if (!ModelState.IsValid)
      {
          return BadRequest(ModelState);
      }

      if (!id.Equals(entity.Id, StringComparison.Ordinal))
      {
          return BadRequest();
      }

      try
      {
        _userService.Update(id, entity);
      }
      catch(DbUpdateConcurrencyException)
      {
        if (!_userService.ApplicationUserExists(id))
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

    // DELETE api/users/5
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {

      return BadRequest();
    } 
  }
}
