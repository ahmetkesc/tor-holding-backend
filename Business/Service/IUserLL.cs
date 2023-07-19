using Model.Classes.App.Result;
using Model.Classes.Database;

namespace Business.Service;

public interface IUserLL
{
    IResult<List<UserLoginLog>> GetUserLogs();
    IResult<List<UserLoginLog>> GetUserLLByEPosta(string eposta);
    IResult<List<UserLoginLog>> GetUserLLByUserId(Guid id);
    IResult<bool> Insert(UserLoginLog user);
    IResult<bool> DeleteAll();
}