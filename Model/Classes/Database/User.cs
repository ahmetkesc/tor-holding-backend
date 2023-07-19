using Dapper.Contrib.Extensions;

namespace Model.Classes.Database;

[Table("t_user")]
public class User
{
    [ExplicitKey] public Guid id { get; set; }

    public string adi { get; set; }
    public string soyadi { get; set; }
    public string cep { get; set; }
    public string adres { get; set; }
    public string eposta { get; set; }
    public string sifre { get; set; }
}