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
    public class TeamsController : ControllerBase
    {
        private readonly FWCDbContext dbContext;
        private readonly IMapper mapper;

        public TeamsController(FWCDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTeams()
        {
            var teams = await dbContext.Teams.ToListAsync();
            return Ok(teams);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetTeamById([FromRoute] Guid id)
        {
            var team = await dbContext.Teams.FirstOrDefaultAsync(t => t.Id == id);
            return Ok(team);
        }

        [HttpPost]
        public async Task<IActionResult> AddTeam([FromBody] TeamDto teamDto)
        {

            Team team = mapper.Map<Team>(teamDto);

            await dbContext.Teams.AddAsync(team);
            await dbContext.SaveChangesAsync();
            return Ok(team);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateTeam([FromRoute] Guid id, [FromBody] TeamDto teamDto)
        {
            var existingTeam = await dbContext.Teams.FirstOrDefaultAsync(t => t.Id == id);
            if (existingTeam == null)
            {
                return NotFound();
            }

            var team = mapper.Map<Team>(teamDto);

            existingTeam.Name = team.Name;
            await dbContext.SaveChangesAsync();
            return Ok(existingTeam);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteTeam([FromRoute] Guid id)
        {
            var team = dbContext.Teams.FirstOrDefault(t => t.Id == id);
            if (team == null)
            {
                return NotFound();
            }
            dbContext.Teams.Remove(team);
            await dbContext.SaveChangesAsync();
            return Ok(team);
        }

    }
}
