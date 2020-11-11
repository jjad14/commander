using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commander.Dtos;
using Commander.Models;
using Microsoft.EntityFrameworkCore;

namespace Commander.Data
{
    public class CommanderRepo : ICommanderRepo
    {
        private readonly CommanderContext _context;
        public CommanderRepo(CommanderContext context)
        {
            _context = context;
        }

        public async void CreateCommand(Command cmd)
        {
            if (cmd == null) {
                throw new ArgumentNullException(nameof(cmd));
            }

            await _context.Commands.AddAsync(cmd);
        }

        public async Task<IEnumerable<Command>> GetAllCommands(int skip, int take)
        {
            var commandItems = await _context.Commands
                .Select(u => new Command {
                    Id = u.Id,
                    Task = u.Task,
                    Platform = u.Platform,
                    PlatformId = u.PlatformId,
                    Instructions = u.Instructions
                })
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return commandItems;
        }

        public async Task<IEnumerable<Command>> GetCommandsByPlatform(int skip, int take, int platoformId)
        {
            var commands = await _context.Commands
                .Select(u => new Command {
                    Id = u.Id,
                    Task = u.Task,
                    Platform = u.Platform,
                    PlatformId = u.PlatformId,
                    Instructions = u.Instructions
                })
                .Where(p => p.PlatformId == platoformId)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return commands;
        }

        public async Task<Command> GetCommandById(int id)
        {
            var command = await _context.Commands
                .Select(u => new Command {
                    Id = u.Id,
                    Task = u.Task,
                    Platform = u.Platform,
                    PlatformId = u.PlatformId,
                    Instructions = u.Instructions
                })
                .FirstOrDefaultAsync(c => c.Id == id);

            return command;
        }

        public async Task<IEnumerable<Platform>> GetAllPlatforms()
        {
            var platformItems = await _context.Platforms
                .Select(p => new Platform {
                    Id = p.Id,
                    Name = p.Name
                })
                .Distinct()
                .OrderBy(p => p.Name)
                .ToListAsync();

            return platformItems;
        }

        public async Task<int> CountAsync()
        {
            return await _context.Commands.CountAsync();
        }

        public async Task<int> CountAsyncForPlatform(int id)
        {
            return await _context.Commands.Where(p => p.PlatformId == id).CountAsync();
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

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

    }
}
