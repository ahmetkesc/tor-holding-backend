using Model.Classes.App;
using Model.Classes.App.Result;

namespace Business.Service;

public interface IAuth
{
    IResult<AccessToken> Login(LoginParameter parameter);
    IResult<AccessToken> RefreshToken(RefreshToken refresh);
}