using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wineapp.Data;
using Wineapp.Models;
using Wineapp.Models.ViewModels;
using Wineapp.Services;

namespace Wineapp.Controllers
{
    public class WinesController : Controller
    {
        private readonly ITastes _tastesServices;
        private readonly IWines _winesServices;
        private readonly IFilters _filtersServices;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;


        public WinesController(ITastes tastesServices, IWines winesServices, IFilters filtersServices, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _tastesServices = tastesServices;
            _winesServices = winesServices;
            _filtersServices = filtersServices;
            _userManager = userManager;
            _signInManager = signInManager;

        }

        public async Task<WinesVM> GetUserPreferences()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            List<ColourTaste> listColourTastes = await _tastesServices.GetColourTasteesByUserIdAsync(user.Id);
            List<SourceTaste> listSourceTastes = await _tastesServices.GetSourceTasteesByUserIdAsync(user.Id);
            List<SweetnessTaste> listSweetnessTastes = await _tastesServices.GetSweetnessTasteesByUserIdAsync(user.Id);

            WinesVM wvm = new WinesVM
            {
                colourTaste = listColourTastes.OrderByDescending(x => x.Score).ToList().First(),
                sourceTaste = listSourceTastes.OrderByDescending(x => x.Score).ToList().First(),
                sweetnessTaste = listSweetnessTastes.OrderByDescending(x => x.Score).ToList().First(),
            };

            return wvm;
    }


        // GET: Wines
        //        public async Task<IActionResult> Index()
        //        {
        //            var applicationDbContext = _context.Wines.Include(w => w.Colour).Include(w => w.Source).Include(w => w.Sweetnes);
        //            return View(await applicationDbContext.ToListAsync());
        //        }

        // GET: Wines/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //        WinesVM wvm = await GetUserPreferences();
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var wine = await _context.Wines
        //        .Include(w => w.Colour)
        //        .Include(w => w.Source)
        //        .Include(w => w.Sweetnes)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (wine == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(wine);
        //}

        //        // GET: Wines/Create
        //        public IActionResult Create()
        //        {
        //            ViewData["ColourId"] = new SelectList(_context.Set<Colour>(), "Id", "ColourType");
        //            ViewData["SourceId"] = new SelectList(_context.Set<Source>(), "Id", "SourceType");
        //            ViewData["SweetnesId"] = new SelectList(_context.Set<Sweetnes>(), "Id", "SweetnesType");
        //            return View();
        //        }

        //        // POST: Wines/Create
        //        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public async Task<IActionResult> Create([Bind("Id,ColourId,SweetnesId,SourceId,Name,Price,Company,Sparkling,Year,Publish")] Wine wine)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                _context.Add(wine);
        //                await _context.SaveChangesAsync();
        //                return RedirectToAction(nameof(Index));
        //            }
        //            ViewData["ColourId"] = new SelectList(_context.Set<Colour>(), "Id", "ColourType", wine.ColourId);
        //            ViewData["SourceId"] = new SelectList(_context.Set<Source>(), "Id", "SourceType", wine.SourceId);
        //            ViewData["SweetnesId"] = new SelectList(_context.Set<Sweetnes>(), "Id", "SweetnesType", wine.SweetnesId);
        //            return View(wine);
        //        }

        //        // GET: Wines/Edit/5
        //        public async Task<IActionResult> Edit(int? id)
        //        {
        //            if (id == null)
        //            {
        //                return NotFound();
        //            }

        //            var wine = await _context.Wines.FindAsync(id);
        //            if (wine == null)
        //            {
        //                return NotFound();
        //            }
        //            ViewData["ColourId"] = new SelectList(_context.Set<Colour>(), "Id", "ColourType", wine.ColourId);
        //            ViewData["SourceId"] = new SelectList(_context.Set<Source>(), "Id", "SourceType", wine.SourceId);
        //            ViewData["SweetnesId"] = new SelectList(_context.Set<Sweetnes>(), "Id", "SweetnesType", wine.SweetnesId);
        //            return View(wine);
        //        }

        //        // POST: Wines/Edit/5
        //        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public async Task<IActionResult> Edit(int id, [Bind("Id,ColourId,SweetnesId,SourceId,Name,Price,Company,Sparkling,Year,Publish")] Wine wine)
        //        {
        //            if (id != wine.Id)
        //            {
        //                return NotFound();
        //            }

        //            if (ModelState.IsValid)
        //            {
        //                try
        //                {
        //                    _context.Update(wine);
        //                    await _context.SaveChangesAsync();
        //                }
        //                catch (DbUpdateConcurrencyException)
        //                {
        //                    if (!WineExists(wine.Id))
        //                    {
        //                        return NotFound();
        //                    }
        //                    else
        //                    {
        //                        throw;
        //                    }
        //                }
        //                return RedirectToAction(nameof(Index));
        //            }
        //            ViewData["ColourId"] = new SelectList(_context.Set<Colour>(), "Id", "ColourType", wine.ColourId);
        //            ViewData["SourceId"] = new SelectList(_context.Set<Source>(), "Id", "SourceType", wine.SourceId);
        //            ViewData["SweetnesId"] = new SelectList(_context.Set<Sweetnes>(), "Id", "SweetnesType", wine.SweetnesId);
        //            return View(wine);
        //        }

        //        // GET: Wines/Delete/5
        //        public async Task<IActionResult> Delete(int? id)
        //        {
        //            if (id == null)
        //            {
        //                return NotFound();
        //            }

        //            var wine = await _context.Wines
        //                .Include(w => w.Colour)
        //                .Include(w => w.Source)
        //                .Include(w => w.Sweetnes)
        //                .FirstOrDefaultAsync(m => m.Id == id);
        //            if (wine == null)
        //            {
        //                return NotFound();
        //            }

        //            return View(wine);
        //        }

        //        // POST: Wines/Delete/5
        //        [HttpPost, ActionName("Delete")]
        //        [ValidateAntiForgeryToken]
        //        public async Task<IActionResult> DeleteConfirmed(int id)
        //        {
        //            var wine = await _context.Wines.FindAsync(id);
        //            _context.Wines.Remove(wine);
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }

        //        private bool WineExists(int id)
        //        {
        //            return _context.Wines.Any(e => e.Id == id);
        //        }
    }
}
