using UserService.Models;

namespace UserService.Data
{
    public class UserRepo : IUserRepo
    {
        private readonly UserContext _context;
        public UserRepo(UserContext context)
        {
            _context = context;
        }
        public void CreateUser(Users user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.users.Add(user);

        }

        public IEnumerable<Users> GetAllUsers()
        {
            return _context.users.ToList();
        }

        public Users GetUserByID(int id)
        {
            return _context.users.FirstOrDefault(p => p.Id == id);
        }

        public bool saveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
