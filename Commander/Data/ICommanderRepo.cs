using System.Collections.Generic;
using System.Threading.Tasks;
using Commander.Models;

namespace Commander.Data
{

    // Change IEnumerable to ICollection?
    public interface ICommanderRepo
    {
        Task<IEnumerable<Command>> GetAllCommands(int skip, int take);
        Task<IEnumerable<Command>> GetCommandsByPlatform(int skip, int take, int platoformId);
        Task<Command> GetCommandById(int id);

        Task<int> CountAsync();
        Task<int> CountAsyncForPlatform(int id);

        void CreateCommand(Command cmd);
        void UpdateCommand(Command cmd);
        void DeleteCommand(Command cmd);

        Task<IEnumerable<Platform>> GetAllPlatforms();

        Task<bool> SaveChanges();
    }
}

// If there are to be future queries and the DB should do the work return IQueryable.
// If there are to be future queries and it is to be done in memory return IEnumerable.
// If there are to be no further queries and all the data will need to be read return an IList, ICollection etc.