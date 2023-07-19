using Autofac;
using Business.Manager;
using Business.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.Classes.App;

namespace Business.Helpers;

public class Delegates
{
    public static Action<ContainerBuilder> ContainerDelegateBuilder(IConfiguration? configuration)
    {
        if (configuration == null) throw new ArgumentNullException(nameof(configuration), "Configuration was not found!");

        var _delegate = new Action<ContainerBuilder>(builder =>
        {
            builder
                .RegisterType<UserManager>().As<IUser>()
                .SingleInstance();
            builder
                .RegisterType<UserLLManager>().As<IUserLL>()
                .SingleInstance();
            builder
                .RegisterType<TokenManager>().As<ITokenBuilder>()
                .SingleInstance();
            builder
                .RegisterType<AuthManager>().As<IAuth>()
                .SingleInstance();
        });

        return _delegate;
    }

    public static Action<JwtBearerOptions> JwtDelegateBuilder(IConfiguration? configuration)
    {
        if (configuration == null) throw new ArgumentNullException(nameof(configuration), "Configuration was not found!");

        var tokenConfig = configuration.GetSection(Constant.AppSettingJwtOptions).Get<TokenConfiguration>();

        if (tokenConfig == null)
            throw new ArgumentNullException(nameof(TokenConfiguration), "Token configuration was not fount!");
        
        var _delegate = new Action<JwtBearerOptions>(option =>
        {
            option.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = tokenConfig.Issuer,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidAudience = tokenConfig.Audience,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = KeyHelper.CreateSecurityKey(tokenConfig.Key),
                ClockSkew = TimeSpan.Zero
            };
        });

        return _delegate;

    }
}