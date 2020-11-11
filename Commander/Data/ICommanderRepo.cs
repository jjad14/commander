using System.Collections.Generic;
using System.Threading.Tasks;
using Commander.Models;

namespace Commander.Data
{
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