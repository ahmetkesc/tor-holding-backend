using Model.Classes.App.Result;
using Model.Classes.Database;

namespace Business.Service;

public interface IUser
{
    IResult<List<User>> GetUsers();
    IResult<User> GetUserByEPosta(string eposta);
    IResult<User> GetUserByEPostaAndPass(string eposta, string pass);
    IResult<User> GetUserById(Guid id);
    IResult<bool> Insert(User user);
    IResult<bool> Update(User user);
    IResult<bool> Delete(User user);
}