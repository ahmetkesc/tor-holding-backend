using Business.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Classes.Database;

namespace API.Controllers;

[Route("userll")]
[Authorize]
[ApiController]
public class UserLogController : ControllerBase
{
    private readonly IUserLL _userLL;

    public UserLogController(IUserLL userLl)
    {
        _userLL = userLl;
    }

    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var result = _userLL.GetUserLogs();
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getuserlogbyeposta")]
    public IActionResult GetUserLLByEPosta(string eposta)
    {
        var result = _userLL.GetUserLLByEPosta(eposta);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getuserlogbyid")]
    public IActionResult GetUserByEPosta(Guid id)
    {
        var result = _userLL.GetUserLLByUserId(id);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("insert")]
    public IActionResult Insert(UserLoginLog userll)
    {
        var result = _userLL.Insert(userll);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("delete")]
    public IActionResult Delete()
    {
        var result = _userLL.DeleteAll();
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }
}