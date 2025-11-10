using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GoSportsAPI.Controllers
{
    /// <summary>
    /// API controller responsible for managing lobbies.
    /// </summary>
    /// <remarks>
    /// Provides endpoints for creating, retrieving, updating, and deleting lobbies.
    /// </remarks>
    [Route("api/lobby")]
    [ApiController]
    public class LobbyController : ControllerBase
    {
        private readonly ILobbyService _lobbySerivce;

        public LobbyController(ILobbyService lobbyService)
        {
            _lobbySerivce = lobbyService;
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

            var lobbies = await _lobbySerivce.GetAllAsync(queryObject);

            return Ok(lobbies);
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

            var lobby = await _lobbySerivce.GetByIdAsync(id);

            if (lobby == null)
            {
                return NotFound();
            }

            return Ok();
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

            var lobby = await _lobbySerivce.GetByIdAsync(id);
            if (lobby == null)
            {
                return NotFound();
            }

            await _lobbySerivce.DeleteAsync(id);

            return NoContent();
        }
    }
}
