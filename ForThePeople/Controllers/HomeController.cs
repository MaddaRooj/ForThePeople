using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ForThePeople.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Newtonsoft.Json;

namespace ForThePeople.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _senatorUrl = "https://api.propublica.org/congress/v1/members/K000388.json";
        private readonly IConfiguration _config;

        public HomeController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public async Task<IActionResult> GetSenator()
        //{
        //    var senator = await GetSenatorAsync();
        //    return View(senator);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //private async Task<ProPublica> GetSenatorAsync()
        //{
        //    var key = _config["ApiKeys:CongressApi"];
        //    var url = $"{_senatorUrl}";
        //    var client = new HttpClient();
        //    client.DefaultRequestHeaders.Add("x-api-key", $"{key}");
        //    var response = await client.GetAsync(url);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var senator = await response.Content.ReadAsAsync<ProPublica>();
        //        return senator;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
    }
}
