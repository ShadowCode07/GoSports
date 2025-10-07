using GoSportsAPI.Data;
using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Mappers;
using GoSportsAPI.Mdels.Lobbies;
using GoSportsAPI.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;

namespace GoSportsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LobbiesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public LobbiesController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Getlobbies()
        {
            var lobbies = await _context.lobbies.ToListAsync();

            var lobbyDto = lobbies.Select(l => l.ToLobbyResponceDto());

            return Ok(lobbyDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLobby([FromRoute] Guid id)
        {
            var lobby = await _context.lobbies.FindAsync(id);

            if (lobby == null)
            {
                return NotFound();
            }

            return Ok(lobby.ToLobbyResponceDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateLobby([FromBody] LobbyCreateDto createDto)
        {
            var lobbyModel = createDto.ToLobbyFromCreate();

            await _context.AddAsync(lobbyModel);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLobby), new { id = lobbyModel.Id }, lobbyModel.ToLobbyResponceDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLobby([FromRoute] Guid id, [FromBody] LobbyUpdateDto updateDto)
        {
            var lobbyModel = await _context.lobbies.FirstOrDefaultAsync(l => l.Id == id);

            if(lobbyModel == null)
            {
                return NotFound();
            }

            lobbyModel = updateDto.ToLobbyFromUpdate();

            await _context.SaveChangesAsync();

            return Ok(lobbyModel.ToLobbyResponceDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLobby(Guid id)
        {
            var lobby = await _context.lobbies.FirstOrDefaultAsync(x => x.Id == id);
            if (lobby == null)
            {
                return NotFound();
            }

            _context.lobbies.Remove(lobby);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
