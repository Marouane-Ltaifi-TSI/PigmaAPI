using PigmaAPI.Entities;
using WebApi.Models;

namespace PigmaAPI.Services.Users
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);

        void Create(User user);
    }
}
