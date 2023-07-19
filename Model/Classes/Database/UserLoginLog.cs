using Dapper.Contrib.Extensions;

namespace Model.Classes.Database;

[Table("t_user_login_log")]
public class UserLoginLog
{
    [ExplicitKey] public Guid id { get; set; }

    public Guid kullanici_id { get; set; }
    public DateTime giris_tarihi { get; set; }
    public DateTime cikis_tarihi { get; set; }
}