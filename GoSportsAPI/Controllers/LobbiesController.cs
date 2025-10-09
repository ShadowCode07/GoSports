using GoSportsAPI.Data;
using GoSportsAPI.Dtos.Lobbies;
using GoSportsAPI.Interfaces;
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
        private readonly ILobbyRepository _repository;

        public LobbiesController(ILobbyRepository lobbyRepository)
        {
            _repository = lobbyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Getlobbies()
        {
            var lobbies = await _repository.GetAllAsync();

            var lobbyDto = lobbies.Select(l => l.ToLobbyResponceDto());

            return Ok(lobbyDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLobby([FromRoute] Guid id)
        {
            var lobby = await _repository.GetByIdAsync(id);

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

            await _repository.CreateAsync(lobbyModel);

            return CreatedAtAction(nameof(GetLobby), new { id = lobbyModel.Id }, lobbyModel.ToLobbyResponceDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLobby([FromRoute] Guid id, [FromBody] LobbyUpdateDto updateDto)
        {
            var lobbyModel = await _repository.GetByIdAsync(id);

            if(lobbyModel == null)
            {
                return NotFound();
            }

            lobbyModel = await _repository.UpdateAsync(id, updateDto);

            return Ok(lobbyModel.ToLobbyResponceDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLobby(Guid id)
        {
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
