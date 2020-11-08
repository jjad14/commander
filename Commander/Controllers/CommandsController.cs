using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repo;
        private readonly IMapper _mapper;
        public CommandsController(ICommanderRepo repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        // GET api/commands
        [HttpGet]
        public ActionResult<IEnumerable<CommandReturnDto>> GetAllCommands()
        {
            var commandItems = _repo.GetAllCommands();

            var commandMaps = _mapper.Map<IEnumerable<CommandReturnDto>>(commandItems);

            return Ok(commandMaps);
        }

        // GET api/commands/{id}
        [HttpGet("{id}", Name="GetCommandById")]
        public ActionResult<CommandReturnDto> GetCommandById(int id)
        {
            var commandItem = _repo.GetCommandById(id);

            if (commandItem == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<CommandReturnDto>(commandItem);

            return Ok(result);
        }

        // GET api/commands/platform/{id}
        [HttpGet("platform/{id}")]
        public ActionResult<CommandReturnDto> GetCommandsByPlatform(int id)
        {
            var commandItem = _repo.GetCommandsByPlatform(id);

            var result = _mapper.Map<IEnumerable<CommandReturnDto>>(commandItem);

            return Ok(result);
        }

        // POST api/commands
        [HttpPost]
        public ActionResult<CommandReturnDto> CreateCommand(CommandCreateDto cmd) 
        {
            var command = _mapper.Map<Command>(cmd);

            _repo.CreateCommand(command);
            _repo.SaveChanges();

            var result = _mapper.Map<CommandReturnDto>(command);

            return CreatedAtRoute(nameof(GetCommandById), new {Id = result.Id}, result);
        }

        // PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto cmd) 
        {
            var commandFromRepo = _repo.GetCommandById(id);

            if (commandFromRepo == null) {
                return NotFound();
            }

            // TODO: Fix mapping
            commandFromRepo.Task = cmd.Task;
            commandFromRepo.Instructions.Description = cmd.Instructions;
            commandFromRepo.Platform.Id = cmd.PlatformId;
            commandFromRepo.Platform.Name = cmd.PlatformName;

            // _mapper.Map(cmd, commandFromRepo);

            _repo.UpdateCommand(commandFromRepo);

            if (!_repo.SaveChanges()) {
                return BadRequest();
            }

            return NoContent();
        }

        // DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandFromRepo = _repo.GetCommandById(id);

            if (commandFromRepo == null) {
                return NotFound();
            }

            _repo.DeleteCommand(commandFromRepo);
            _repo.SaveChanges();

            return NoContent();
        }

        // GET: api/commands/platforms
        [HttpGet("platforms")]
        public ActionResult<IEnumerable<PlatformReturnDto>> GetAllPlatforms() 
        {
            var platformItems = _repo.GetAllPlatforms();

            var platformMaps = _mapper.Map<IEnumerable<PlatformReturnDto>>(platformItems);

            return Ok(platformMaps);
        }

    }
}