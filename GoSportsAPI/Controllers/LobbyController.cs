using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Extensions;
using GoSportsAPI.Helpers;
using GoSportsAPI.Interfaces.IServices;
using GoSportsAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoSportsAPI.Controllers
{
    /// <summary>
    /// API controller responsible for managing lobbies.
    /// </summary>
    /// <remarks>
    /// Provides endpoints for creating, retrieving, updating, and deleting lobbies.
    /// </remarks>
    [Authorize]
    [Route("api/lobby")]
    [ApiController]
    public class LobbyController : ControllerBase
    {
        private readonly ILobbyService _lobbyService;

        public LobbyController(ILobbyService lobbyService)
        {
            _lobbyService = lobbyService;
        }

        /// <summary>
        /// Gets all lobbies based on filtering and sorting options.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] LobbyQueryObject queryObject)
        {
            var lobbies = await _lobbyService.GetAllAsync(queryObject);
            return Ok(lobbies);
        }

        /// <summary>
        /// Gets a lobby by ID.
        /// </summary>
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var lobby = await _lobbyService.GetByIdAsync(id);
            
            if (lobby is null)
            {
                return NotFound();
            }


            return Ok(lobby);
        }

        /// <summary>
        /// Creates a new lobby.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LobbyCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.GetUserId();
            if(userId == null)
            {
                return Unauthorized();
            }

            var created = await _lobbyService.CreateAsync(dto, userId.Value);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing lobby.
        /// </summary>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] LobbyUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await _lobbyService.UpdateAsync(id, dto);

            if (updated is null)
            {
                return NotFound();
            }


            return Ok(updated);
        }

        /// <summary>
        /// Joins the given lobby as the specified user profile.
        /// NOTE: userProfileId is currently passed explicitly; 
        /// later you can get it from the authenticated user instead.
        /// </summary>
        [HttpPost("{id:guid}/join")]
        public async Task<IActionResult> Join(Guid id)
        {
            var userId = User.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var success = await _lobbyService.JoinLobbyAsync(id, userId.Value);

            if (!success)
            {
                return BadRequest("Unable to join lobby (it may be full, missing, or user already left).");
            }

            return NoContent();
        }

        /// <summary>
        /// Leaves the given lobby as the specified user profile.
        /// </summary>
        [HttpPost("{id:guid}/leave")]
        public async Task<IActionResult> Leave(Guid id)
        {
            var userId = User.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var success = await _lobbyService.LeaveLobbyAsync(id, userId.Value);

            if (!success)
            {

                return BadRequest("Unable to leave lobby (it may not exist, or user is not a member).");
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a lobby by ID.
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var exists = await _lobbyService.GetByIdAsync(id);
            if (exists is null)
                return NotFound();

            var deleted = await _lobbyService.DeleteAsync(id);
            if (!deleted)
                return BadRequest("Could not delete lobby.");

            return NoContent();
        }
    }
}
