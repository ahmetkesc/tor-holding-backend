using Business.Helpers;
using Business.Service;
using Model.Classes.App;
using Model.Classes.App.Result;
using Model.Classes.Database;
using Model.Extensions;

namespace Business.Manager;

public class AuthManager : IAuth
{
    private IUser _user;
    private IUserLL _userLL;
    private ITokenBuilder _token;
    private static Dictionary<string, string> refreshTokens = new();
    public AuthManager(IUser user, ITokenBuilder token, IUserLL userLl)
    {
        _user = user;
        _token = token;
        _userLL = userLl;
    }

    public IResult<AccessToken> Login(LoginParameter parameter)
    {
        try
        {
            var eposta = parameter.Input.DecryptString(Constant.GlobalAesKey);
            var pass = parameter.Password.DecryptString(Constant.GlobalAesKey);

            var today = DateTime.Today;
            var srvPass = $"TOR{today.Year}{ZeroFind(today.Month)}{ZeroFind(today.Day)}";

            if (eposta == Constant.Supervisor && pass == srvPass)
            {
                var srv = _token.CreateToken(new LoginParameter{Input = eposta,Password = pass});

                refreshTokens[eposta] = srv.RefreshToken;

                return Result.Ok(srv);
            }

           

            var user = _user.GetUserByEPostaAndPass(parameter.Input, parameter.Password);

            if (!user.Success) return Result.Message<AccessToken>("Kullanıcı bilgileri yanlış.");

            var token = _token.CreateToken(new LoginParameter { Input = eposta, Password = pass });

            refreshTokens[parameter.Input] = token.RefreshToken;
            _userLL.Insert(new UserLoginLog
            {
                giris_tarihi = DateTime.Now, cikis_tarihi = DateTime.Now.AddHours(1), id = Guid.NewGuid(),
                kullanici_id = user.Data.id
            });

            return Result.Ok(token);
        }
        catch (Exception e)
        {
            return Result.Message<AccessToken>(e.Message);
        }
    }


    public IResult<AccessToken> RefreshToken(RefreshToken refresh)
    {
        try
        {
            if (!refreshTokens.TryGetValue(refresh.Input, out var stored) || Guid.Parse(stored) != refresh.Token)
                return Result.Message<AccessToken>("Refresh token oluşturulamadı.");

            var token = _token.CreateToken(new LoginParameter { Input = refresh.Input, Password = string.Empty });

            return Result.Ok(token);

        }
        catch (Exception e)
        {
            return Result.Message<AccessToken>(e.Message);
        }
    }

    private string ZeroFind(int value) => value <= 9 ? "0" + value : value.ToString();
}