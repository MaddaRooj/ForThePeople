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
    public class LocalRepsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly string _allLocalRepsUrl = "https://www.googleapis.com/civicinfo/v2/representatives?address=Nashville%2C%20Tennessee&key=AIzaSyCt2l4J5up5wGSGMhrVGPgqDGPsyye69H0";
        private readonly IConfiguration _config;

        public LocalRepsController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: LocalReps
        public async Task<IActionResult> Index()
        {
            var localReps = await GetAllLocalRepsAsync();
            return View(localReps);
        }

        private async Task<LocalRep> GetAllLocalRepsAsync()
        {
            //var key = _config["ApiKeys:CongressApi"];
            var url = $"{_allLocalRepsUrl}";
            var client = new HttpClient();
            //client.DefaultRequestHeaders.Add("x-api-key", $"{key}");
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var localReps = await response.Content.ReadAsAsync<LocalRep>();
                return localReps;
            }
            else
            {
                return null;
            }
        }

        // GET: LocalReps/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var localRep = await _context.LocalRep
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (localRep == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(localRep);
        //}
    }
}
