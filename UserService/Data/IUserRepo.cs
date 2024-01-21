using UserService.Models;

namespace UserService.Data
{
    public interface IUserRepo
    {
        bool saveChanges();

        IEnumerable<Users> GetAllUsers();
        Users GetUserByID(int id);
        void CreateUser(Users user);
        void DeleteUser(Users user);
        Users GetUserByUid(string uid);

    }
}
