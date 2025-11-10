using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Interfaces.IServices;
using GoSportsAPI.Services;
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
        private readonly ILobbyService _lobbyService;
        private readonly LocationLobbiesService _locationLobbieService;

        public LocationLobbiesController(LocationLobbiesService locationLobbieService, ILobbyService lobbyService)
        {
            _locationLobbieService = locationLobbieService;
            _lobbyService = lobbyService;
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

            if (!await _locationLobbieService.CheckLobby(locationGuid))
            {
                return NotFound("Location doesn't exist");
            }

            if (!await _locationLobbieService.CheckLobbyCount(locationGuid))
            {
                return BadRequest("Lobby count full for this location");
            }

            var lobbyModel = _locationLobbieService.CreateAsync(locationGuid, createDto);

            return CreatedAtRoute(
                //routeName: "GetLobby",
                routeValues: new { lobbyId = lobbyModel.Id },
                value: lobbyModel
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

            var lobbyModel = await _lobbyService.GetByIdAsync(id);

            if (lobbyModel == null)
            {
                return NotFound();
            }

            lobbyModel = await _locationLobbieService.UpdateAsync(locationGuid, id, updateDto);

            return Ok(lobbyModel);
        }
    }
}
