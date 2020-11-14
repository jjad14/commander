using System.Collections.Generic;
using System.Threading.Tasks;
using Commander.Models;

namespace Commander.Data.Identity
{
    public class UserRepo : IUserRepo
    {
        private readonly UserContext _context;

        public UserRepo(UserContext context)
        {
            _context = context;
        }

        public void CreateUser(User cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteUser(User cmd)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetUserById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsers()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateUser(User cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}