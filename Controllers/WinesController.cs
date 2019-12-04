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
       
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITastes _tastesServices;
        private readonly IFilters _filtersServices;


        public WinesController(SignInManager<AppUser> signInManager,UserManager<AppUser> userManager, ITastes tastesServices, IFilters filtersServices)
        {           
            _signInManager = signInManager;
            _userManager = userManager;
            _tastesServices = tastesServices;
            _filtersServices = filtersServices;
        }
        public async Task GetUserPreferences()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            List<ColourTaste> colourTastes = await _tastesServices.GetColourTastesAsync();
            
            List<SourceTaste> sourceTastes = await _context.SourceTastes.Where(x => x.AppUserId == user.Id).ToListAsync();
            List<SweetnessTaste> sweetnessTaste = await _context.SweetnessTastes.Where(x => x.AppUserId == user.Id).ToListAsync();
        }
        // GET: Wines
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Wines.Include(w => w.Colour).Include(w => w.Source).Include(w => w.Sweetnes);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Wines/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (_signInManager.IsSignedIn(User))
            {


                if (id == null)
                {
                    return NotFound();
                }
                WineVM wvm = new WineVM();
                wvm.Wine = await _context.Wines.FindAsync(id);

                if ()



            }

            var wine = await _context.Wines
                .Include(w => w.Colour)
                .Include(w => w.Source)
                .Include(w => w.Sweetnes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wine == null)
            {
                return NotFound();
            }

            return View(wine);
        }

        //// GET: Wines/Create
        //public IActionResult Create()
        //{
        //    ViewData["ColourId"] = new SelectList(_context.Set<Colour>(), "Id", "ColourType");
        //    ViewData["SourceId"] = new SelectList(_context.Set<Source>(), "Id", "SourceType");
        //    ViewData["SweetnesId"] = new SelectList(_context.Set<Sweetnes>(), "Id", "SweetnesType");
        //    return View();
        //}

        //// POST: Wines/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,ColourId,SweetnesId,SourceId,Name,Price,Company,Sparkling,Year,Publish")] Wine wine)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(wine);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ColourId"] = new SelectList(_context.Set<Colour>(), "Id", "ColourType", wine.ColourId);
        //    ViewData["SourceId"] = new SelectList(_context.Set<Source>(), "Id", "SourceType", wine.SourceId);
        //    ViewData["SweetnesId"] = new SelectList(_context.Set<Sweetnes>(), "Id", "SweetnesType", wine.SweetnesId);
        //    return View(wine);
        //}

        //// GET: Wines/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var wine = await _context.Wines.FindAsync(id);
        //    if (wine == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["ColourId"] = new SelectList(_context.Set<Colour>(), "Id", "ColourType", wine.ColourId);
        //    ViewData["SourceId"] = new SelectList(_context.Set<Source>(), "Id", "SourceType", wine.SourceId);
        //    ViewData["SweetnesId"] = new SelectList(_context.Set<Sweetnes>(), "Id", "SweetnesType", wine.SweetnesId);
        //    return View(wine);
        //}

        //// POST: Wines/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,ColourId,SweetnesId,SourceId,Name,Price,Company,Sparkling,Year,Publish")] Wine wine)
        //{
        //    if (id != wine.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(wine);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!WineExists(wine.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ColourId"] = new SelectList(_context.Set<Colour>(), "Id", "ColourType", wine.ColourId);
        //    ViewData["SourceId"] = new SelectList(_context.Set<Source>(), "Id", "SourceType", wine.SourceId);
        //    ViewData["SweetnesId"] = new SelectList(_context.Set<Sweetnes>(), "Id", "SweetnesType", wine.SweetnesId);
        //    return View(wine);
        //}

        //// GET: Wines/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
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

        //// POST: Wines/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var wine = await _context.Wines.FindAsync(id);
        //    _context.Wines.Remove(wine);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool WineExists(int id)
        //{
        //    return _context.Wines.Any(e => e.Id == id);
        //}
    }
}
