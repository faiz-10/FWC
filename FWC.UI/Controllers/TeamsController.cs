using FWC.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FWC.UI.Controllers
{
    public class TeamsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public TeamsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {

            List<Team> teams = new List<Team>();

            var client = httpClientFactory.CreateClient();

            var httpResponseMessage = await client.GetAsync("https://localhost:7035/api/teams");
            
            httpResponseMessage.EnsureSuccessStatusCode();

            teams.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<Team>>());

            return View(teams);
        }
    }
}
