using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wineapp.Data;
using Wineapp.Models;

namespace Wineapp.Controllers
{
    public class WineListsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WineListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WineLists
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.WineLists.Include(w => w.AppUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: WineLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wineList = await _context.WineLists
                .Include(w => w.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wineList == null)
            {
                return NotFound();
            }

            return View(wineList);
        }

        // GET: WineLists/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id");
            return View();
        }

        // POST: WineLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ListName,Description,ListDate,AppUserId")] WineList wineList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wineList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", wineList.AppUserId);
            return View(wineList);
        }

        // GET: WineLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wineList = await _context.WineLists.FindAsync(id);
            if (wineList == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", wineList.AppUserId);
            return View(wineList);
        }

        // POST: WineLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ListName,Description,ListDate,AppUserId")] WineList wineList)
        {
            if (id != wineList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wineList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WineListExists(wineList.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", wineList.AppUserId);
            return View(wineList);
        }

        // GET: WineLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wineList = await _context.WineLists
                .Include(w => w.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wineList == null)
            {
                return NotFound();
            }

            return View(wineList);
        }

        // POST: WineLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wineList = await _context.WineLists.FindAsync(id);
            _context.WineLists.Remove(wineList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WineListExists(int id)
        {
            return _context.WineLists.Any(e => e.Id == id);
        }
    }
}
