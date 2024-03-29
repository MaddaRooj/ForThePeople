﻿using System;
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
    public class SenateController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly string _allSenatorsUrl = "https://api.propublica.org/congress/v1/116/senate/members.json";
        private readonly string _senatorUrl = "https://api.propublica.org/congress/v1/members/";
        private readonly IConfiguration _config;

        public SenateController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: Senate
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            //TEST
            ViewBag.CurrentSort = sortOrder;

            //Creates view bag for sort parameter
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.StateSortParm = sortOrder == "State" ? "state_desc" : "State";

            //Declares variable senate and gets all senators from api
            var senate = await GetAllSenatorsAsync();

            // If there is no search input then begin on page 1, else filter by search params
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            //Sets all members to members variable
            var members = from m in senate.Results.First().Members
                          select m;

            // Search input LINQ parameters
            if (!String.IsNullOrEmpty(searchString))
            {
                members = members.Where(m => m.Last_Name.Contains(searchString)
                                       || m.First_Name.Contains(searchString)
                                       || m.Full_Name.Contains(searchString)
                                       || m.State.Contains(searchString));
            }

            // Switch statement for sorting functionality
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

            // Detemines number of results per page and initial page view
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(members.ToPagedList(pageNumber, pageSize));
        }

        // API query that will return all US senators
        private async Task<Senate> GetAllSenatorsAsync()
        {
            var key = _config["ApiKeys:CongressApi"];
            var url = $"{_allSenatorsUrl}";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-api-key", $"{key}");
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var senators = await response.Content.ReadAsAsync<Senate>();
                return senators;
            }
            else
            {
                return null;
            }
        }


        // API query that will get a specific senator
        private async Task<Result> GetSenatorAsync(string memberId)
        {
            var key = _config["ApiKeys:CongressApi"];
            var url = $"{_senatorUrl}{memberId}.json";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-api-key", $"{key}");
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var senator = await response.Content.ReadAsAsync<Result>();
                return senator;
            }
            else
            {
                return null;
            }
        }

        //Returns details view for a specific Senator
        [Authorize]
        public async Task<IActionResult> Details(string Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var senator = await GetSenatorAsync(Id);

            if (senator == null)
            {
                return NotFound();
            }
            return View(senator);
        }
    }
}
