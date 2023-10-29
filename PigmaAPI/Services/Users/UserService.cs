
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PigmaAPI.Entities;
using PigmaAPI.Infrastructure.ApplicationDbContext;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Helpers;
using WebApi.Models;

namespace PigmaAPI.Services.Users;
public class UserService : IUserService
{
    // users hardcoded for simplicity, store in a db with hashed passwords in production applications


    private readonly AppSettings _appSettings;
    private readonly ApplicationDbContext _context;


    public UserService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
    {
        _appSettings = appSettings.Value;
        _context = context;

    }

    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        var user = _context.Users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

        // return null if user not found
        if (user == null) return null;

        // authentication successful so generate jwt token
        var token = generateJwtToken(user);

        return new AuthenticateResponse(user, token);
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users;
    }

    public User GetById(int id)
    {
        return _context.Users.FirstOrDefault(x => x.Id == id);
    }
    public void Create(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        
    }
    // helper methods

    private string generateJwtToken(User user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }


}