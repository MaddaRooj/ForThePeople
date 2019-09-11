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
    public class LegislatureController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly string _allIntroducedLegislatureUrl = "https://api.propublica.org/congress/v1/116/house/bills/active.json";
        private readonly IConfiguration _config;

        public LegislatureController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: Legislature
        public async Task<IActionResult> Index()
        {
            var legislature = await GetAllLegislatureAsync();
            var bills = from b in legislature.Results.First().Bills
                          select b;
            return View(bills);
        }

        private async Task<Legislature> GetAllLegislatureAsync()
        {
            var key = _config["ApiKeys:CongressApi"];
            var url = $"{_allIntroducedLegislatureUrl}";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-api-key", $"{key}");
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var legislature = await response.Content.ReadAsAsync<Legislature>();
                return legislature;
            }
            else
            {
                return null;
            }
        }

        //// GET: Legislatures/Details/5
        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var legislature = await _context.Legislature
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (legislature == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(legislature);
        //}
    }
}
