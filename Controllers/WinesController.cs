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
                ListUserColourTaste = listColourTastes.OrderByDescending(x => x.Score).ToList().GetRange(0, 3),
                ListUserSourceTaste = listSourceTastes.OrderByDescending(x => x.Score).ToList().GetRange(0, 3),
                ListUserSweetnessTaste = listSweetnessTastes.OrderByDescending(x => x.Score).ToList().GetRange(0, 3),
            };

            return wvm;
        }

        public async Task<IActionResult> SourcePreferences(int sourceId)
        {
            sourceId = 1;
            WinesVM wvm = await GetUserPreferences();
            wvm.Source = await _filtersServices.GetSourceByIdAsync(sourceId);
            wvm.ListSources = await _filtersServices.GetSourceAsync();

            List<Wine> wines = await _winesServices.GetWinesAsync();
            wines = wines.Where(x => x.Source.Id == sourceId).ToList();

            List<Wine> firstPreference = new List<Wine>();
            List<Wine> secondPreference = new List<Wine>();


            if (_signInManager.IsSignedIn(User))
            {
                foreach (Wine wine in wines)
                {
                    if (wvm.ListUserColourTaste[0].ColourId == wine.ColourId && wvm.ListUserSweetnessTaste[0].SweetnesId == wine.SweetnesId)
                    {
                        firstPreference.Add(wine);
                    }
                    else if (wvm.ListUserColourTaste[0].ColourId == wine.ColourId && wvm.ListUserSweetnessTaste[1].SweetnesId == wine.SweetnesId)
                    {
                        secondPreference.Add(wine);
                    }
                    else if (wvm.ListUserColourTaste[1].ColourId == wine.ColourId && wvm.ListUserSweetnessTaste[1].SweetnesId == wine.SweetnesId)
                    {
                        secondPreference.Add(wine);
                    }
                }

            }
            else
            {
                firstPreference = await _winesServices.GetWinesAsync();
            }

            Random numberRandom = new Random();
            if (firstPreference.Count > 0)
            {              
                for (int i = 0; i < 1; i++)
                {
                    int num = numberRandom.Next(0, firstPreference.Count);
                    wvm.ListWinesTastesSources.Add(firstPreference[num]);
                }

            }
            if (secondPreference.Count > 0)
            {
                for (int i = 0; i < 1; i++)
                {
                    int num = numberRandom.Next(0, secondPreference.Count);
                    wvm.ListWinesTastesSources.Add(secondPreference[num]);
                }

            }

            if (wvm.ListWinesTastesSources==null)
            {
                wvm.ListWinesTastesSources = wines;
            }

            return View(wvm);
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
