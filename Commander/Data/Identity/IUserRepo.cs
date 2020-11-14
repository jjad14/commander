using System.Collections.Generic;
using System.Threading.Tasks;
using Commander.Models;

namespace Commander.Data.Identity
{
    public interface IUserRepo
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(int id);

        void CreateUser(User cmd);
        void UpdateUser(User cmd);
        void DeleteUser(User cmd);

        Task<bool> SaveChanges();


        // reset password

        // send email
    }
}