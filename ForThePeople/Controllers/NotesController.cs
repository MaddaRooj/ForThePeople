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
using Microsoft.AspNetCore.Identity;
using System.Net.Http;

namespace ForThePeople.Controllers
{
    public class NotesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly string _senatorUrl = "https://api.propublica.org/congress/v1/members/";


        public NotesController(ApplicationDbContext context, IConfiguration config, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _config = config;
            _userManager = userManager;
        }

        // GET: Notes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Note.ToListAsync());
        }

        //GET: Notes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content,ApplicationUserId,RepId")] Note note)
        {
            var senator = await GetMemberAsync(note.RepId);
            
            note.ApplicationUserId = _userManager.GetUserId(HttpContext.User);
            if (ModelState.IsValid)
            {
                _context.Add(note);
                await _context.SaveChangesAsync();
                if(senator.Results.First().Roles.First().Chamber == "Senate")
                {
                    return RedirectToAction("Details", "Senate", new { Id = senator.Results.First().Member_Id});
                }
                else
                {
                    return RedirectToAction("Details", "House", new { Id = senator.Results.First().Member_Id});
                }
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLegislatureNote([Bind("Title,Content,ApplicationUserId,LegislationId")] Note note)
        {
            note.ApplicationUserId = _userManager.GetUserId(HttpContext.User);
            if (ModelState.IsValid)
            {
                _context.Add(note);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Legislature");
            }
            return NotFound();
        }

        // GET: Notes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Note.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }
            return View(note);
        }

        // GET: Notes/EditLegislatureNote/5
        public async Task<IActionResult> EditLegislatureNote(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Note.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }
            return View(note);
        }

        // POST: Notes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,ApplicationUserId,RepId")] Note note)
        {
            var senator = await GetMemberAsync(note.RepId);

            if (id != note.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(note);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteExists(note.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (senator.Results.First().Roles.First().Chamber == "Senate")
                {
                    return RedirectToAction("Details", "Senate", new { Id = senator.Results.First().Member_Id });
                }
                else
                {
                    return RedirectToAction("Details", "House", new { Id = senator.Results.First().Member_Id });
                }
            }
            return View(note);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLegislatureNote(int id, [Bind("Id,Title,Content,ApplicationUserId,LegislationId")] Note note)
        {
            if (id != note.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(note);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteExists(note.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Legislature");
            }
            return View(note);
        }

        // GET: Notes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Note
                .FirstOrDefaultAsync(m => m.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var note = await _context.Note.FindAsync(id);
            var member = await GetMemberAsync(note.RepId);
            _context.Note.Remove(note);
            await _context.SaveChangesAsync();
            if (note.LegislationId == null)
            {
                if(member.Results.First().Roles.First().Chamber == "Senate")
                {
                    return RedirectToAction("Details", "Senate", new { Id = member.Results.First().Member_Id});
                }
                else
                {
                    return RedirectToAction("Details", "House", new { Id = member.Results.First().Member_Id});
                }
            }
            else
            {
                return RedirectToAction("Index", "Legislature");
            }
        }

        // GET: Notes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Note
                .FirstOrDefaultAsync(m => m.Id == id);

            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        private bool NoteExists(int id)
        {
            return _context.Note.Any(e => e.Id == id);
        }

        private async Task<Result> GetMemberAsync(string memberId)
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
    }
}
