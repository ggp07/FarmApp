using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FarmAppV2.Data;
using FarmAppV2.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace FarmAppV2.Controllers
{
    [Authorize]
    public class SheepController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SheepController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sheep
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var applicationDbContext = _context.Sheep.Where(i => i.OwnerId == userId).Include(s => s.SheepVaccines);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Sheep/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sheep = await _context.Sheep
                .Include(s => s.Owner)
                .FirstOrDefaultAsync(m => m.SheepId == id);
            if (sheep == null)
            {
                return NotFound();
            }

            return View(sheep);
        }

        // GET: Sheep/Create
        public IActionResult Create()
        {
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Sheep/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SheepId,SheepName,Gender,BirthDay,MothersName,FathersName,Miscarriage,OwnerId")] Sheep sheep)
        {
            if (ModelState.IsValid)
            {
                sheep.OwnerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(sheep);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", sheep.OwnerId);
            return View(sheep);
        }

        // GET: Sheep/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sheep = await _context.Sheep.FindAsync(id);
            if (sheep == null)
            {
                return NotFound();
            }
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", sheep.OwnerId);
            return View(sheep);
        }

        // POST: Sheep/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SheepId,SheepName,Gender,BirthDay,MothersName,FathersName,Miscarriage,OwnerId")] Sheep sheep)
        {
            if (id != sheep.SheepId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sheep);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SheepExists(sheep.SheepId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", sheep.OwnerId);
            return View(sheep);
        }

        // GET: Sheep/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sheep = await _context.Sheep
                .Include(s => s.Owner)
                .FirstOrDefaultAsync(m => m.SheepId == id);
            if (sheep == null)
            {
                return NotFound();
            }

            return View(sheep);
        }

        // POST: Sheep/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sheep = await _context.Sheep.FindAsync(id);
            _context.Sheep.Remove(sheep);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SheepExists(int id)
        {
            return _context.Sheep.Any(e => e.SheepId == id);
        }
    }
}
