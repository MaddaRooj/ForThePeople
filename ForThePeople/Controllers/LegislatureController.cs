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
using Microsoft.AspNetCore.Authorization;

namespace ForThePeople.Controllers
{
    public class LegislatureController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly string _allIntroducedLegislatureUrl = "https://api.propublica.org/congress/v1/116/house/bills/active.json";
        private readonly string _allPassedLegislatureUrl = "https://api.propublica.org/congress/v1/116/house/bills/passed.json";
        private readonly string _allVetoedLegislatureUrl = "https://api.propublica.org/congress/v1/116/house/bills/vetoed.json";
        private readonly string _specificLegislatureUrl = "https://api.propublica.org/congress/v1/116/bills/";
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
        
        // GET: Legislature
        public async Task<IActionResult> PassedLegislatureIndex()
        {
            var legislature = await GetAllPassedLegislatureAsync();
            var bills = from b in legislature.Results.First().Bills
                          select b;
            return View(bills);
        }
        
        // GET: Legislature
        public async Task<IActionResult> VetoedLegislatureIndex()
        {
            var legislature = await GetAllVetoedLegislatureAsync();
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

        private async Task<Legislature> GetAllPassedLegislatureAsync()
        {
            var key = _config["ApiKeys:CongressApi"];
            var url = $"{_allPassedLegislatureUrl}";
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

        private async Task<Legislature> GetAllVetoedLegislatureAsync()
        {
            var key = _config["ApiKeys:CongressApi"];
            var url = $"{_allVetoedLegislatureUrl}";
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

        private async Task<Result> GetSpecificBillAsync(string billId)
        {
            var key = _config["ApiKeys:CongressApi"];
            var url = $"{_specificLegislatureUrl}{billId}.json";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-api-key", $"{key}");
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var bill = await response.Content.ReadAsAsync<Result>();
                return bill;
            }
            else
            {
                return null;
            }
        }

        [Authorize]
        // GET: Legislature/Details/5
        public async Task<IActionResult> Details(string Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var legislature = await GetSpecificBillAsync(Id);
            if (legislature == null)
            {
                return NotFound();
            }

            return View(legislature);
        }
    }
}
