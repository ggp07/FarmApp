using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FarmAppV2.Data;
using FarmAppV2.Models;

namespace FarmAppV2.Controllers
{
    public class VaccinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VaccinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vaccines
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Vaccine.Include(v => v.Owner);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Vaccines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccine = await _context.Vaccine
                .Include(v => v.Owner)
                .FirstOrDefaultAsync(m => m.VaccineId == id);
            if (vaccine == null)
            {
                return NotFound();
            }

            return View(vaccine);
        }

        // GET: Vaccines/Create
        public IActionResult Create()
        {
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Vaccines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VaccineId,Name,VaccineDate,OwnerId")] Vaccine vaccine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaccine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", vaccine.OwnerId);
            return View(vaccine);
        }

        // GET: Vaccines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccine = await _context.Vaccine.FindAsync(id);
            if (vaccine == null)
            {
                return NotFound();
            }
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", vaccine.OwnerId);
            return View(vaccine);
        }

        // POST: Vaccines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VaccineId,Name,VaccineDate,OwnerId")] Vaccine vaccine)
        {
            if (id != vaccine.VaccineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaccine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaccineExists(vaccine.VaccineId))
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
            ViewData["OwnerId"] = new SelectList(_context.Users, "Id", "Id", vaccine.OwnerId);
            return View(vaccine);
        }

        // GET: Vaccines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccine = await _context.Vaccine
                .Include(v => v.Owner)
                .FirstOrDefaultAsync(m => m.VaccineId == id);
            if (vaccine == null)
            {
                return NotFound();
            }

            return View(vaccine);
        }

        // POST: Vaccines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vaccine = await _context.Vaccine.FindAsync(id);
            _context.Vaccine.Remove(vaccine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VaccineExists(int id)
        {
            return _context.Vaccine.Any(e => e.VaccineId == id);
        }
    }
}
