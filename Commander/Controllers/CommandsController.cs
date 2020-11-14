using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Commander.helpers;
using Commander.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

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
        public async Task<ActionResult<Pagination<IEnumerable<CommandReturnDto>>>> GetAllCommands([FromQuery] CommandParams commandParams)
        {  
            IEnumerable<Command> commandItems = new List<Command>();
            int totalItems = 0;

            // TODO: Specification pattern
            if (commandParams.PlatformId.HasValue) {
                commandItems = await _repo.GetCommandsByPlatform(
                    (commandParams.PageSize * (commandParams.PageIndex - 1)), 
                    commandParams.PageSize, 
                    commandParams.PlatformId.GetValueOrDefault());
                totalItems = await _repo.CountAsyncForPlatform(commandParams.PlatformId.GetValueOrDefault());
            }
            else {
                commandItems = await _repo.GetAllCommands(
                    (commandParams.PageSize * (commandParams.PageIndex - 1)), 
                    commandParams.PageSize);
                totalItems = await _repo.CountAsync();
            }

            var data = _mapper.Map<IEnumerable<CommandReturnDto>>(commandItems);

            return Ok(new Pagination<CommandReturnDto>(commandParams.PageIndex, commandParams.PageSize, totalItems, data));
        }

        // GET api/commands/{id}
        [HttpGet("{id}", Name="GetCommandById")] 
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CommandReturnDto>> GetCommandById(int id)
        {
            var commandItem = await _repo.GetCommandById(id);

            if (commandItem == null)
            {
                return NotFound(new ApiResponse(404));
            }

            var result = _mapper.Map<CommandReturnDto>(commandItem);

            return Ok(result);
        }

        // POST api/commands
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CommandReturnDto>> CreateCommand(CommandCreateDto cmd) 
        {
            var command = _mapper.Map<Command>(cmd);

            _repo.CreateCommand(command);
            
            if (!await _repo.SaveChanges()) {
                return BadRequest(new ApiResponse(409));
            }

            var result = _mapper.Map<CommandReturnDto>(command);

            return CreatedAtRoute(nameof(GetCommandById), new {Id = result.Id}, result);
        }

        // PUT api/commands/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
        public async Task<ActionResult> UpdateCommand(int id, CommandUpdateDto cmd) 
        {
            var commandFromRepo = await _repo.GetCommandById(id);

            if (commandFromRepo == null) {
                return NotFound(new ApiResponse(404));
            }

            // TODO: Fix mapping
            commandFromRepo.Task = cmd.Task;
            commandFromRepo.Instructions.Description = cmd.Instructions;
            commandFromRepo.Platform.Id = cmd.PlatformId;
            commandFromRepo.Platform.Name = cmd.PlatformName;

            // _mapper.Map(cmd, commandFromRepo);

            _repo.UpdateCommand(commandFromRepo);

            if (!await _repo.SaveChanges()) {
                return BadRequest(new ApiResponse(409, "Resource could not be updated."));
            }

            return NoContent();
        }

        // DELETE api/commands/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
        public async Task<ActionResult> DeleteCommand(int id)
        {
            var commandFromRepo = await _repo.GetCommandById(id);

            if (commandFromRepo == null) {
                return NotFound(new ApiResponse(404, "Resource not found."));
            }

            _repo.DeleteCommand(commandFromRepo);

            if (!await _repo.SaveChanges()) {
                return BadRequest(new ApiResponse(409, "Resource could not be deleted."));
            }

            return NoContent();
        }

        // GET: api/commands/platforms
        [HttpGet("platforms")]
        public async Task<ActionResult<IEnumerable<PlatformReturnDto>>> GetAllPlatforms() 
        {
            var platformItems = await _repo.GetAllPlatforms();

            var platformMaps = _mapper.Map<IEnumerable<PlatformReturnDto>>(platformItems);

            return Ok(platformMaps);
        }

    }
}