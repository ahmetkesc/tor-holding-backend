using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Business.Helpers;
using Business.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.Classes.App;
using Constant = Model.Classes.App.Constant;

namespace Business.Manager;

public class TokenManager : ITokenBuilder
{
    private DateTime _accessTokenExpiration;
    private readonly TokenConfiguration _tokenConfiguration;

    public TokenManager(IConfiguration configuration)
    {
        _tokenConfiguration = configuration.GetSection(Constant.AppSettingJwtOptions).Get<TokenConfiguration>() ?? new TokenConfiguration
        {
            AccessTokenExpiration = 750,
            Audience = "www.torholding.com",
            Key = "TorHoldingSecurityKey",
            Issuer = "www.torholding.com.tr"
        };
    }
    public AccessToken CreateToken(LoginParameter parameters)
    {
        _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenConfiguration.AccessTokenExpiration);

        var key = KeyHelper.CreateSecurityKey(_tokenConfiguration.Key);
        var creadentials = KeyHelper.CreateSigningCredentials(key);
        var jwt = CreateJwtToken(_tokenConfiguration, creadentials, parameters);
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.WriteToken(jwt);
        var refreshToken = Guid.NewGuid().ToString();

        return new AccessToken { Expiration = _accessTokenExpiration, Token = token, RefreshToken = refreshToken };
    }

    private SecurityToken CreateJwtToken(TokenConfiguration configuration, SigningCredentials credentials,
        LoginParameter parameters)
    {
        var jwt = new JwtSecurityToken(
            audience: configuration.Audience,
            issuer: configuration.Issuer,
            expires: _accessTokenExpiration,
            notBefore: DateTime.Now,
            signingCredentials: credentials,
            claims: new List<Claim> { new(ClaimTypes.Name, parameters.Input), new(ClaimTypes.Role, "User") }
        );
        return jwt;
    }
}