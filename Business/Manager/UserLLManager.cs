using Business.Helpers;
using Business.Service;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.Classes.App;
using Model.Classes.App.Result;
using Model.Classes.Database;
using Model.Extensions;
using Npgsql;

namespace Business.Manager;

public class UserLLManager : IUserLL
{
    private readonly IUser _user;
    private readonly string connectionString = "";

    public UserLLManager(IConfiguration configuration, IUser user)
    {
        _user = user;
        connectionString = configuration.GetConnectionString(Constant.ConnectionName) ?? "";
    }

    public IResult<List<UserLoginLog>> GetUserLogs()
    {
        try
        {
            if (connectionString.IsNullOrEmpty())
                return Result.Message<List<UserLoginLog>>("Veri tabanı bağlantı cümlesi bulunamadı.");

            using var db = new NpgsqlConnection(connectionString);
            db.CheckAndOpenDatabase();

            var result = db.Query<UserLoginLog>("select * from t_user_login_log").ToList();

            return Result.Ok(result);
        }
        catch (Exception e)
        {
            return Result.Message<List<UserLoginLog>>(e.Message);
        }
    }

    public IResult<List<UserLoginLog>> GetUserLLByEPosta(string eposta)
    {
        try
        {
            if (connectionString.IsNullOrEmpty())
                return Result.Message<List<UserLoginLog>>("Veri tabanı bağlantı cümlesi bulunamadı.");

            using var db = new NpgsqlConnection(connectionString);
            db.CheckAndOpenDatabase();

            var user = _user.GetUserByEPosta(eposta.EncryptString(Constant.GlobalAesKey));

            if (!user.Success) return Result.Message<List<UserLoginLog>>("Kullanıcı bulunamadı.");

            var result = db.Query<UserLoginLog>("select * from t_user_login_log where kullanici_id=@id ::uuid",
                new { user.Data.id }).ToList();

            return Result.Ok(result);
        }
        catch (Exception e)
        {
            return Result.Message<List<UserLoginLog>>(e.Message);
        }
    }

    public IResult<List<UserLoginLog>> GetUserLLByUserId(Guid id)
    {
        try
        {
            if (connectionString.IsNullOrEmpty())
                return Result.Message<List<UserLoginLog>>("Veri tabanı bağlantı cümlesi bulunamadı.");

            using var db = new NpgsqlConnection(connectionString);
            db.CheckAndOpenDatabase();

            var result = db
                .Query<UserLoginLog>("select * from t_user_login_log where kullanici_id=id ::uuid", new { id })
                .ToList();

            return Result.Ok(result);
        }
        catch (Exception e)
        {
            return Result.Message<List<UserLoginLog>>(e.Message);
        }
    }

    public IResult<bool> Insert(UserLoginLog userll)
    {
        try
        {
            if (connectionString.IsNullOrEmpty())
                return Result.Message<bool>("Veri tabanı bağlantı cümlesi bulunamadı.");

            using var db = new NpgsqlConnection(connectionString);
            db.CheckAndOpenDatabase();

            userll.id = Guid.NewGuid();


            var result = db.Insert(userll);
            return
                result != 0
                    ? Result.Message<bool>("Kullanıcı logu kayıt edilemedi.")
                    : Result.Ok(true);
        }
        catch (Exception e)
        {
            return Result.Message<bool>(e.Message);
        }
    }

    public IResult<bool> DeleteAll()
    {
        try
        {
            if (connectionString.IsNullOrEmpty())
                return Result.Message<bool>("Veri tabanı bağlantı cümlesi bulunamadı.");

            using var db = new NpgsqlConnection(connectionString);
            db.CheckAndOpenDatabase();

            var result = db.DeleteAll<UserLoginLog>();
            return
                result
                    ? Result.Message<bool>("Kullanıcı logları silinemedi.")
                    : Result.Ok(true);
        }
        catch (Exception e)
        {
            return Result.Message<bool>(e.Message);
        }
    }
}