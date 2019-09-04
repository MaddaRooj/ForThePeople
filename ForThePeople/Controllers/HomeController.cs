using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ForThePeople.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace ForThePeople.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _repsUrl = "https://www.googleapis.com/civicinfo/v2/representatives?address=nashville&key=";
        private readonly string _electionsUrl = "https://www.googleapis.com/civicinfo/v2/elections?key=";
        private readonly IConfiguration _config;

        public HomeController(IConfiguration config)
        {
            _config = config;
        }
        public async Task<IActionResult> Index()
        {
            var elections = await GetElectionsAsync();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<Election> GetElectionsAsync()
        {
            var key = _config["ApiKeys:GoogleCivicApi"];
            var url = $"{_electionsUrl}{key}";
            var client = new HttpClient();
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var election = await response.Content.ReadAsAsync<Election>();
                return election;
            }
            else
            {
                return null;
            }
        }
    }
}
