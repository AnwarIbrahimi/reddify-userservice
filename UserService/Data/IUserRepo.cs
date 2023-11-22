using UserService.Models;

namespace UserService.Data
{
    public interface IUserRepo
    {
        bool saveChanges();

        IEnumerable<User> GetAllUsers();
        User GetUserByID(int id);
        void CreateUser(User user);

    }
}
