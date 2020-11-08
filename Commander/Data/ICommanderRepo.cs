using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public interface ICommanderRepo
    {
        IEnumerable<Command> GetAllCommands();
        IEnumerable<Command> GetCommandsByPlatform(int id);
        Command GetCommandById(int id);
        void CreateCommand(Command cmd);
        void UpdateCommand(Command cmd);
        void DeleteCommand(Command cmd);

        IEnumerable<Platform> GetAllPlatforms();
        Platform GetPlatformById(int id);

        bool SaveChanges();


    }
}