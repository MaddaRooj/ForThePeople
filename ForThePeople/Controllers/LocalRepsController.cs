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
        private readonly string _allLocalRepsUrl = "https://www.googleapis.com/civicinfo/v2/representatives?address=";
        private readonly IConfiguration _config;

        public LocalRepsController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: LocalReps
        // Add search string to index which will pass into GetAllLocalRepsAsync()
        public async Task<IActionResult> Index(string searchString)
        {
            var localReps = await GetAllLocalRepsAsync(searchString);
            return View(localReps);
        }

        // searchString will in theory alter the api get method to include the location in the url
        private async Task<LocalRep> GetAllLocalRepsAsync(string searchString)
        {
            var key = _config["ApiKeys:GoogleCivicApi"];
            if (searchString == null)
            {
                searchString = "Nashville";
            }
            else
            {
                searchString = searchString;
            }
            var url = $"{_allLocalRepsUrl}{searchString}&key={key}";
            var client = new HttpClient();
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
