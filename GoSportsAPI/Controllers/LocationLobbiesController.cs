using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Interfaces;
using GoSportsAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace GoSportsAPI.Controllers
{
    [ApiController]
    [Route("locations/{locationGuid:guid}/lobbies")]
    public class LocationLobbiesController : ControllerBase
    {
        private readonly ILobbyRepository _repository;
        private readonly ILocationRepository _locationRepository;

        public LocationLobbiesController(ILobbyRepository repository, ILocationRepository locationRepository)
        {
            _repository = repository;
            _locationRepository = locationRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLobby([FromRoute] Guid locationGuid, [FromBody] LobbyCreateDto createDto)
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

            await _repository.CreateAsync(lobbyModel, createDto.SportName);

            await _locationRepository.AddLobbyToCount(locationGuid, lobbyModel.Id);

            return CreatedAtRoute(
                //routeName: "GetLobby",
                routeValues: new { lobbyId = lobbyModel.Id },
                value: lobbyModel.ToLobbyResponceDto()
            );
        }
    }
}
