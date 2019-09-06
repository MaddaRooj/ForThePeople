using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ForThePeople.Data;
using ForThePeople.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace ForThePeople.Controllers
{
    public class ProPublicasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly string _allSenatorsUrl = "https://api.propublica.org/congress/v1/116/senate/members.json";
        private readonly string _senatorUrl = "https://api.propublica.org/congress/v1/members/K000388.json";
        private readonly IConfiguration _config;

        public ProPublicasController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: ProPublicas
        public async Task<IActionResult> Index()
        {
            var senators = await GetAllSenatorsAsync();
            return View(senators);
        }

        public async Task<IActionResult> GetSenator()
        {
            var senator = await GetSenatorAsync();
            return View(senator);
        }

        ////GET: ProPublicas/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var proPublica = await _context.ProPublica
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (proPublica == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(proPublica);
        //}

        private async Task<Senator> GetAllSenatorsAsync()
        {
            var key = _config["ApiKeys:CongressApi"];
            var url = $"{_allSenatorsUrl}";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-api-key", $"{key}");
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var senators = await response.Content.ReadAsAsync<Senator>();
                return senators;
            }
            else
            {
                return null;
            }
        }

        private async Task<ProPublica> GetSenatorAsync()
        {
            var key = _config["ApiKeys:CongressApi"];
            var url = $"{_senatorUrl}";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-api-key", $"{key}");
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var senator = await response.Content.ReadAsAsync<ProPublica>();
                return senator;
            }
            else
            {
                return null;
            }
        }
    }
}
