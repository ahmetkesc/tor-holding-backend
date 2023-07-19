using Model.Classes.App;

namespace Business.Service;

public interface ITokenBuilder
{
    AccessToken CreateToken(LoginParameter parameters);
}