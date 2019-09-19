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
using X.PagedList;
using Microsoft.AspNetCore.Authorization;

namespace ForThePeople.Controllers
{
    public class HouseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly string _allHouseUrl = "https://api.propublica.org/congress/v1/116/house/members.json";
        private readonly string _senatorUrl = "https://api.propublica.org/congress/v1/members/";
        private readonly IConfiguration _config;

        public HouseController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: House of Representatives
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            //TEST
            ViewBag.CurrentSort = sortOrder;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.StateSortParm = sortOrder == "State" ? "state_desc" : "State";

            var senate = await GetAllHouseMembersAsync();

            //TEST
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var members = from m in senate.Results.First().Members
                          select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                members = members.Where(m => m.Last_Name.Contains(searchString)
                                       || m.First_Name.Contains(searchString)
                                       || m.Full_Name.Contains(searchString)
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

            int pageSize = 15;
            int pageNumber = (page ?? 1);
            //return View(members);
            return View(members.ToPagedList(pageNumber, pageSize));
        }

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

        [Authorize]
        //GET: ProPublicas/Details/5
        public async Task<IActionResult> Details(string Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var member = await GetHouseMemberAsync(Id);

            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        private async Task<Result> GetHouseMemberAsync(string memberId)
        {
            var key = _config["ApiKeys:CongressApi"];
            var url = $"{_senatorUrl}{memberId}.json";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-api-key", $"{key}");
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var member = await response.Content.ReadAsAsync<Result>();
                return member;
            }
            else
            {
                return null;
            }
        }
    }
}
