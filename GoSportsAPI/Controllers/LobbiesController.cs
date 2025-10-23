using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Dtos.Locations;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces;
using GoSportsAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace GoSportsAPI.Controllers
{
    /// <summary>
    /// API controller responsible for managing lobbies.
    /// </summary>
    /// <remarks>
    /// Provides endpoints for creating, retrieving, updating, and deleting lobbies.
    /// </remarks>
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

        /// <summary>
        /// Retrieves a collection of lobbies based on the specified query parameters.
        /// </summary>
        /// <param name="queryObject">The query object used for filtering and sorting lobbies.</param>
        /// <returns>
        /// Returns an <see cref="ActionResult{T}"/> containing a collection of <see cref="LobbyResponceDto"/> objects.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LobbyResponceDto>>> Getlobbies([FromQuery] LobbyQueryObject queryObject)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lobbies = await _repository.GetAllAsync(queryObject);

            var lobbyDto = lobbies.Select(l => l.ToLobbyResponceDto());

            return Ok(lobbyDto);
        }

        /// <summary>
        /// Retrieves a lobby with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the lobby to retrieve.</param>
        /// <returns>
        /// Returns an <see cref="ActionResult{T}"/> containing the <see cref="LobbyResponceDto"/> if found; otherwise, a not found result.
        /// </returns>
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

        /// <summary>
        /// Updates an existing lobby with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the lobby to update.</param>
        /// <param name="updateDto">The data transfer object containing the updated lobby information.</param>
        /// <returns>
        /// Returns an <see cref="IActionResult"/> indicating the result of the update operation.
        /// </returns>
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

        /// <summary>
        /// Deletes a lobby with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the lobby to delete.</param>
        /// <returns>
        /// Returns an <see cref="IActionResult"/> indicating the result of the delete operation.
        /// </returns>
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
