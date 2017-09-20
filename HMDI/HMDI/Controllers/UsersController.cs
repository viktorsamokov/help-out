using AutoMapper;
using HMDI.Dtos;
using HMDI.Entities;
using HMDI.Helpers;
using HMDI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HMDI.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  public class UsersController : Controller
  {
    private IApplicationUserService _userService;
    private IMapper _mapper;

    public UsersController(IApplicationUserService userService, IMapper mapper)
    {
      _userService = userService;
      _mapper = mapper;
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
    public async Task<IActionResult> PostUser([FromBody] RegisterDto entity)
    {
      if (!ModelState.IsValid)
      {
         return BadRequest(ModelState);
      }

      ApplicationUser user = await _userService.Create(entity);

      return Ok(user);
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] LoginDto model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest();
      }

      var user = await _userService.FindUserByEmail(model);

      if(user == null)
      {
        return BadRequest();
      }

      PasswordVerificationResult result = _userService.VerifyHashedPassword(user, model.Password);

      if(result != PasswordVerificationResult.Success)
      {
        return BadRequest();
      }
      
      JwtSecurityToken token = await _userService.GetJwtSecurityToken(user);

      LoggedInUser loggedInUser = _mapper.Map<LoggedInUser>(user);

      return Ok(new
      {
        token = new JwtSecurityTokenHandler().WriteToken(token),
        expiration = token.ValidTo,
        user = loggedInUser
      });

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
