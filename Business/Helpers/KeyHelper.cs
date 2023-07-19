using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Business.Helpers;

public static class KeyHelper
{
    public static SecurityKey CreateSecurityKey(string key)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    }

    public static SigningCredentials CreateSigningCredentials(SecurityKey key)
    {
        return new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
    }
}