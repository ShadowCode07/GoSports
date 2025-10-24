using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Interfaces;
using GoSportsAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace GoSportsAPI.Controllers
{
    /// <summary>
    /// API controller responsible for managing lobbies within a specific location.
    /// </summary>
    /// <remarks>
    /// Provides endpoints for creating, retrieving, updating, and deleting lobbies associated with a given location.
    /// </remarks>
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

        /// <summary>
        /// Creates a new lobby for the specified location.
        /// </summary>
        /// <param name="locationGuid">The unique identifier of the location where the lobby will be created.</param>
        /// <param name="createDto">The data transfer object containing the details of the lobby to create.</param>
        /// <returns>
        /// Returns an <see cref="IActionResult"/> containing the result of the create operation.
        /// </returns>
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

            await _repository.CreateAsync(locationGuid, lobbyModel, createDto.SportName);

            await _locationRepository.AddLobbyToCount(locationGuid, lobbyModel.Id);

            return CreatedAtRoute(
                //routeName: "GetLobby",
                routeValues: new { lobbyId = lobbyModel.Id },
                value: lobbyModel.ToLobbyResponceDto()
            );
        }

        /// <summary>
        /// Updates an existing lobby with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the lobby to update.</param>
        /// <param name="updateDto">The data transfer object containing the updated lobby information.</param>
        /// <returns>
        /// Returns an <see cref="IActionResult"/> indicating the result of the update operation.
        /// </returns>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateLobby([FromRoute] Guid locationGuid, [FromRoute] Guid id, [FromBody] LobbyUpdateDto updateDto)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lobbyModel = await _repository.GetByIdAsync(id);

            if (lobbyModel == null)
            {
                return NotFound();
            }

            lobbyModel = await _repository.UpdateAsync(locationGuid, id, updateDto, updateDto.SportName);

            return Ok(lobbyModel.ToLobbyResponceDto());
        }
    }
}
