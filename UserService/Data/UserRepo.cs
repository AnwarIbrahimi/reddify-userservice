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
        public void CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.user.Add(user);

        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.user.ToList();
        }

        public User GetUserByID(int id)
        {
            return _context.user.FirstOrDefault(p => p.Id == id);
        }

        public bool saveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
