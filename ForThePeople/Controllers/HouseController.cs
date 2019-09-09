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
    public class HouseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly string _allHouseUrl = "https://api.propublica.org/congress/v1/116/house/members.json";
        //private readonly string _oneMemberUrl = "https://api.propublica.org/congress/v1/members/A000360.json";
        private readonly IConfiguration _config;

        public HouseController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: Senate
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.StateSortParm = sortOrder == "State" ? "state_desc" : "State";

            var senate = await GetAllHouseMembersAsync();

            var members = from m in senate.Results.First().Members
                          select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                members = members.Where(m => m.Last_Name.Contains(searchString)
                                       || m.First_Name.Contains(searchString)
                                       || m.State.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    members = members.OrderByDescending(s => s.Last_Name);
                    break;
                case "State":
                    members = members.OrderBy(m => m.State);
                    break;
                case "state_desc":
                    members = members.OrderByDescending(m => m.State);
                    break;
                default:
                    members = members.OrderBy(m => m.Last_Name);
                    break;
            }
            return View(members);
        }

        //public async Task<IActionResult> GetSenator()
        //{
        //    var senator = await GetSenatorAsync();
        //    return View(senator);
        //}

        ////GET: ProPublicas/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var proPublica = await GetSenatorAsync().Result
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (proPublica == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(proPublica);
        //}

        private async Task<House> GetAllHouseMembersAsync()
        {
            var key = _config["ApiKeys:CongressApi"];
            var url = $"{_allHouseUrl}";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-api-key", $"{key}");
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var houseMembers = await response.Content.ReadAsAsync<House>();
                return houseMembers;
            }
            else
            {
                return null;
            }
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
