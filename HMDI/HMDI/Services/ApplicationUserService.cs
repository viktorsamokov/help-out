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
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;

namespace HMDI.Services
{
  public interface IApplicationUserService
  {
    ApplicationUser GetById(string id);
    Task<ApplicationUser> Create(RegisterDto registerDto);
    void Update(string id, ApplicationUser entity);
    ApplicationUser Delete(string id);
    bool ApplicationUserExists(string id);
    Task<ApplicationUser> FindUserByEmail(LoginDto model);
    PasswordVerificationResult VerifyHashedPassword(ApplicationUser user, string password);
    Task<JwtSecurityToken> GetJwtSecurityToken(ApplicationUser user);
    List<FavoriteAgenda> GetFavorites(string userId);
  }

  public class ApplicationUserService : IApplicationUserService
  {
    private readonly ApplicationDbContext _db;
    private UserManager<ApplicationUser> _userManager;
    private IUrlHelper _urlHelper;
    private IPasswordHasher<ApplicationUser> _passwordHasher;
    private IOptions<AppSettings> _appSettings;

    public ApplicationUserService(ApplicationDbContext db, UserManager<ApplicationUser> userManager, 
      IUrlHelper urlHelper, IPasswordHasher<ApplicationUser> passwordHasher, IOptions<AppSettings> appSettings)
    {
      _db = db;
      _userManager = userManager;
      _urlHelper = urlHelper;
      _passwordHasher = passwordHasher;
      _appSettings = appSettings;
    }

    public bool ApplicationUserExists(string id)
    {
      return _db.Users.Count(e => e.Id.Equals(id, StringComparison.Ordinal)) > 0;
    }

    public List<FavoriteAgenda> GetFavorites(string userId)
    {
      ApplicationUser user = _db.Users.Include(u => u.Favorites).ThenInclude(a => a.Agenda)
        .ThenInclude(a => a.User)
        .Include(u => u.Favorites).ThenInclude(a => a.Agenda)
        .ThenInclude(a => a.Items)
        .Where(u => u.Id == userId).FirstOrDefault();

      List<FavoriteAgenda> favorites = user.Favorites.ToList();

      return favorites;
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

    public async Task<ApplicationUser> FindUserByEmail(LoginDto model)
    {
      ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);

      return user;
    }

    public ApplicationUser GetById(string id)
    {
      return _db.Users.Include(u => u.AgendaCategories).Where(u => u.Id == id).FirstOrDefault();
    }

    public async Task<JwtSecurityToken> GetJwtSecurityToken(ApplicationUser user)
    {
      IList<Claim> userClaims = await _userManager.GetClaimsAsync(user);
 
      return new JwtSecurityToken(
          issuer: _appSettings.Value.SiteUrl,
          audience: _appSettings.Value.SiteUrl,
          claims: GetTokenClaims(user).Union(userClaims),
          expires: DateTime.UtcNow.AddDays(10),
          signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Value.SecretKey)), SecurityAlgorithms.HmacSha256)
      );
    }

    public void Update(string id, ApplicationUser entity)
    {
      ApplicationUser user = _db.Users.Find(id);

      user.FirstName = entity.FirstName;
      user.LastName = entity.LastName;

      _db.Entry(user).State = EntityState.Modified;
      _db.SaveChanges();
    }

    public PasswordVerificationResult VerifyHashedPassword(ApplicationUser user, string password)
    {
      return _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
    }

    #region getTokenClaims
    private static IEnumerable<Claim> GetTokenClaims(ApplicationUser user)
      {
      return new List<Claim>
        {
          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
          new Claim(JwtRegisteredClaimNames.Sub, user.Id)
        };
      }

   
    #endregion
  }
}
