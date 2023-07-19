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

public class UserManager : IUser
{
    private readonly string connectionString = "";

    public UserManager(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString(Constant.ConnectionName) ?? "";
    }

    public IResult<List<User>> GetUsers()
    {
        try
        {
            if (connectionString.IsNullOrEmpty())
                return Result.Message<List<User>>("Veri tabanı bağlantı cümlesi bulunamadı.");

            using var db = new NpgsqlConnection(connectionString);
            db.CheckAndOpenDatabase();

            var result = db.Query<User>("select * from t_user").ToList();

            return Result.Ok(result);
        }
        catch (Exception e)
        {
            return Result.Message<List<User>>(e.Message);
        }
    }

    public IResult<User> GetUserByEPosta(string eposta)
    {
        try
        {
            if (connectionString.IsNullOrEmpty())
                return Result.Message<User>("Veri tabanı bağlantı cümlesi bulunamadı.");

            using var db = new NpgsqlConnection(connectionString);
            db.CheckAndOpenDatabase();

            var result =
                db.QueryFirstOrDefault<User>("select * from t_user where eposta=@eposta ::text", new { eposta });

            return Result.Ok(result);
        }
        catch (Exception e)
        {
            return Result.Message<User>(e.Message);
        }
    }

    public IResult<User> GetUserByEPostaAndPass(string eposta, string pass)
    {
        try
        {
            if (connectionString.IsNullOrEmpty())
                return Result.Message<User>("Veri tabanı bağlantı cümlesi bulunamadı.");

            using var db = new NpgsqlConnection(connectionString);
            db.CheckAndOpenDatabase();


            var result = db.QueryFirstOrDefault<User>(
                "select * from t_user where eposta=@eposta ::text AND sifre=@sifre ::text",
                new { eposta, sifre = pass });

            return Result.Ok(result);
        }
        catch (Exception e)
        {
            return Result.Message<User>(e.Message);
        }
    }

    public IResult<User> GetUserById(Guid id)
    {
        try
        {
            if (connectionString.IsNullOrEmpty())
                return Result.Message<User>("Veri tabanı bağlantı cümlesi bulunamadı.");

            using var db = new NpgsqlConnection(connectionString);
            db.CheckAndOpenDatabase();

            var result = db.QueryFirstOrDefault<User>("select * from t_user where id=@id::text", new { id });

            return Result.Ok(result);
        }
        catch (Exception e)
        {
            return Result.Message<User>(e.Message);
        }
    }

    public IResult<bool> Insert(User user)
    {
        try
        {
            if (connectionString.IsNullOrEmpty())
                return Result.Message<bool>("Veri tabanı bağlantı cümlesi bulunamadı.");

            using var db = new NpgsqlConnection(connectionString);
            db.CheckAndOpenDatabase();

            user.id = Guid.NewGuid();
            user.eposta = user.eposta.EncryptString(Constant.GlobalAesKey);
            user.sifre = user.sifre.EncryptString(Constant.GlobalAesKey);

            var result = db.Insert(user);
            return
                result != 0
                    ? Result.Message<bool>("Kullanıcı kayıt edilemedi.")
                    : Result.Ok(true);
        }
        catch (Exception e)
        {
            return Result.Message<bool>(e.Message);
        }
    }

    public IResult<bool> Update(User user)
    {
        try
        {
            if (connectionString.IsNullOrEmpty())
                return Result.Message<bool>("Veri tabanı bağlantı cümlesi bulunamadı.");

            using var db = new NpgsqlConnection(connectionString);
            db.CheckAndOpenDatabase();

            user.eposta = user.eposta.EncryptString(Constant.GlobalAesKey);
            user.sifre = user.sifre.EncryptString(Constant.GlobalAesKey);

            var result = db.Update(user);
            return
                result
                    ? Result.Ok(true)
                    : Result.Message<bool>("Kullanıcı güncellenemedi.");
        }
        catch (Exception e)
        {
            return Result.Message<bool>(e.Message);
        }
    }

    public IResult<bool> Delete(User user)
    {
        try
        {
            if (connectionString.IsNullOrEmpty())
                return Result.Message<bool>("Veri tabanı bağlantı cümlesi bulunamadı.");

            using var db = new NpgsqlConnection(connectionString);
            db.CheckAndOpenDatabase();

            var result = db.Delete(user);
            return
                result
                    ? Result.Ok(true)
                    : Result.Message<bool>("Kullanıcı silinemedi.");
        }
        catch (Exception e)
        {
            return Result.Message<bool>(e.Message);
        }
    }
}