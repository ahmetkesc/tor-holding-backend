using Business.Service;
using Microsoft.AspNetCore.Mvc;
using Model.Classes.App;

namespace API.Controllers;

[Route("auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private IAuth _auth;

    public AuthController(IAuth auth)
    {
        _auth = auth;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginParameter parameter)
    {
        var result = _auth.Login(parameter);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("refresh")]
    public IActionResult Refresh(RefreshToken refresh)
    {
        var result = _auth.RefreshToken(refresh);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }
}