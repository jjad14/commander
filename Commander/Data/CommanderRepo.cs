using System;
using System.Collections.Generic;
using System.Linq;
using Commander.Dtos;
using Commander.Models;

namespace Commander.Data
{
    public class CommanderRepo : ICommanderRepo
    {
        private readonly CommanderContext _context;
        public CommanderRepo(CommanderContext context)
        {
            _context = context;
        }

        public void CreateCommand(Command cmd)
        {
            if (cmd == null) {
                throw new ArgumentNullException(nameof(cmd));
            }

            _context.Commands.Add(cmd);
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commandItems = _context.Commands
                .Select(u => new Command {
                    Id = u.Id,
                    Task = u.Task,
                    Platform = u.Platform,
                    Instructions = u.Instructions
                }) 
                .ToList();

            return commandItems;
        }

        public Command GetCommandById(int id)
        {
            var command = _context.Commands
                .Select(u => new Command {
                    Id = u.Id,
                    Task = u.Task,
                    Platform = u.Platform,
                    Instructions = u.Instructions
                })
                .ToList()
                .FirstOrDefault(c => c.Id == id);

            return command;
        }

        public void UpdateCommand(Command cmd)
        {    
            _context.Commands.Update(cmd);
        }

        public void DeleteCommand(Command cmd)
        {
            if (cmd == null) {
                throw new ArgumentNullException(nameof(cmd));
            }
            
            _context.Commands.Remove(cmd);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            var platformItems = _context.Platforms
                .Select(p => new Platform {
                    // Id = p.Id,
                    Name = p.Name
                })
                .Distinct()
                .OrderBy(p => p.Name)
                .ToList();

            return platformItems;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

    }
}
