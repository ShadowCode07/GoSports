using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Dtos.Locations;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces;
using GoSportsAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace GoSportsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LobbiesController : ControllerBase
    {
        private readonly ILobbyRepository _repository;
        private readonly ILocationRepository _locationRepository;

        public LobbiesController(ILobbyRepository lobbyRepository, ILocationRepository locationRepository)
        {
            _repository = lobbyRepository;
            _locationRepository = locationRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LobbyResponceDto>>> Getlobbies([FromQuery]LobbyQueryObject queryObject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lobbies = await _repository.GetAllAsync(queryObject);

            var lobbyDto = lobbies.Select(l => l.ToLobbyResponceDto());

            return Ok(lobbyDto);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<LobbyResponceDto>> GetLobby([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lobby = await _repository.GetByIdAsync(id);

            if (lobby == null)
            {
                return NotFound();
            }

            return Ok(lobby.ToLobbyResponceDto());
        }

        // Move to locations controllers
        /*[HttpPost("{locationGuid}")]
        public async Task<IActionResult> CreateLobby([FromRoute] Guid locationGuid, LobbyCreateDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _locationRepository.Exists(locationGuid))
            {
                return NotFound("Location doesn't exist");
            }

            if (!await _locationRepository.CheckLobbyCount(locationGuid))
            {
                return BadRequest("Lobby count full for this location");
            }

            var lobbyModel = createDto.ToLobbyFromCreate(locationGuid);

            await _repository.CreateAsync(lobbyModel);

            await _locationRepository.AddLobbyToCount(locationGuid);

            return CreatedAtAction(
                nameof(GetLobby),
                new { id = lobbyModel.Id }, 
                lobbyModel.ToLobbyResponceDto());
        }*/

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateLobby([FromRoute] Guid id, [FromBody] LobbyUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lobbyModel = await _repository.GetByIdAsync(id);

            if(lobbyModel == null)
            {
                return NotFound();
            }

            lobbyModel = await _repository.UpdateAsync(id, updateDto, updateDto.SportName);

            return Ok(lobbyModel.ToLobbyResponceDto());
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteLobby(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lobby = await _repository.GetByIdAsync(id);
            if (lobby == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);

            return NoContent();
        }

    }
}
