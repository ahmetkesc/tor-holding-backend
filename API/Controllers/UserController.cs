using Business.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Classes.App;
using Model.Classes.Database;

namespace API.Controllers;

[Route("user"),Authorize]
[ApiController]
public class UserController : ControllerBase
{
    private IUser _user;

    public UserController(IUser user)
    {
        _user = user;
    }

    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var result = _user.GetUsers();
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }
    [HttpGet("getuserbyeposta")]
    public IActionResult GetUserByEPosta(string eposta)
    {
        var result = _user.GetUserByEPosta(eposta);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }
    [HttpGet("getuserbyid")]
    public IActionResult GetUserByEPosta(Guid id)
    {
        var result = _user.GetUserById(id);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }
    [HttpPost("insert")]
    public IActionResult Insert(User user)
    {
        var result = _user.Insert(user);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }
    [HttpPost("update")]
    public IActionResult Update(User user)
    {
        var result = _user.Update(user);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }
    [HttpPost("delete")]
    public IActionResult Delete(User user)
    {
        var result = _user.Delete(user);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }
}