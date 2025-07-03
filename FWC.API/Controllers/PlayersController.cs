using AutoMapper;
using FWC.API.Data;
using FWC.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FWC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly FWCDbContext dbContext;
        private readonly IMapper mapper;

        public PlayersController(FWCDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlayers() 
        {
            var players = await dbContext.Players.Include(p => p.Team).ToListAsync();
            return Ok(players);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetPlayerById([FromRoute] Guid id)
        {
            var player = await dbContext.Players.Include(p => p.Team).FirstOrDefaultAsync(x => x.Id == id);
            if (player == null) { return NotFound(); }
            return Ok(player);
        }

        [HttpPost]
        public async Task<IActionResult> AddPlayer([FromBody] PlayerDto playerDto)
        {

            var player = mapper.Map<Player>(playerDto);

            await dbContext.Players.AddAsync(player);
            await dbContext.SaveChangesAsync();

            return Ok(player);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdatePlayer([FromRoute] Guid id, [FromBody] PlayerDto playerDto)
        {

            var player = await dbContext.Players.Include(p => p.Team).FirstOrDefaultAsync(p => p.Id == id);
            if(player == null) { return NotFound(); }

            player.FirstName = playerDto.FirstName;
            player.LastName = playerDto.LastName;
            player.Position = playerDto.Position;

            await dbContext.SaveChangesAsync();

            return Ok(player);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeletePlayer([FromRoute] Guid id)
        {
            var player = await dbContext.Players.Include(p => p.Team).FirstOrDefaultAsync(p => p.Id == id);
            if (player == null) { return NotFound(); }

            dbContext.Players.Remove(player);
            await dbContext.SaveChangesAsync();

            return Ok(player);
        }
    }
}
