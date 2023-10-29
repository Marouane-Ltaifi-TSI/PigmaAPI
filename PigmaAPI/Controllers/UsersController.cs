
using Microsoft.AspNetCore.Mvc;
using PigmaAPI.Services.Users;
using PigmaAPI.Entities;
using PigmaAPI.Helpers;
using WebApi.Models;
namespace PigmaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("authenticate")]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
        var response = _userService.Authenticate(model);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }
    [HttpPost]
    public IActionResult Create(User User)
    {
        _userService.Create(User);
        return Ok();
    }
}
