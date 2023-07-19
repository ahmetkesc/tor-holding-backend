namespace Model.Classes.App;

public class TokenConfiguration
{
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public int AccessTokenExpiration { get; set; }
    public string Key { get; set; }
}