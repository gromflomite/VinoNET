﻿using System;
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
                ListUserColourTaste = listColourTastes.OrderByDescending(x => x.Score).ToList().GetRange(0,3),
                ListUserSourceTaste = listSourceTastes.OrderByDescending(x => x.Score).ToList().GetRange(0, 3),
                ListUserSweetnessTaste = listSweetnessTastes.OrderByDescending(x => x.Score).ToList().GetRange(0, 3),
            };

            return wvm;
        }


        // GET: Wines
        public async Task<IActionResult> Index()
        {
            List<Wine> listWines = await _winesServices.GetWinesAsync(); 
            return View(listWines);
        }

        //GET: Wines/Details/5
        public async Task<IActionResult> Details()
        {
            int? id = 4;
            if (id == null)
            {
                return NotFound();
            }
            WinesVM wvm = await GetUserPreferences();
            wvm.Wine = await _winesServices.GetWineByIdAsync(id);


            List<Wine> listOne = new List<Wine>();
            List<Wine> listTwo = new List<Wine>();
            List<Wine> listThree = new List<Wine>();

            int colourTasteOne = wvm.ListUserColourTaste[0].ColourId;
            int sweetnessTasteOne = wvm.ListUserSweetnessTaste[0].SweetnesId;
            int sourceTasteOne = wvm.ListUserSourceTaste[0].SourceId;


            //Filtro para sacar los vinos segun gustos
            foreach (Wine wine in await _winesServices.GetWinesAsync())
            {
                if (wine.ColourId == wvm.Wine.ColourId && wine.SweetnesId == wvm.ListUserSweetnessTaste[0].SweetnesId && wine.SourceId == wvm.ListUserSourceTaste[0].SourceId)
                {
                    listOne.Add(wine);
                }
                else if (wine.ColourId == wvm.Wine.ColourId && wine.SweetnesId == wvm.ListUserSweetnessTaste[1].SweetnesId && wine.SourceId == wvm.ListUserSourceTaste[1].SourceId)
                {
                    listOne.Add(wine);
                }
                else if (wine.ColourId == wvm.Wine.ColourId && wine.SweetnesId == wvm.ListUserSweetnessTaste[2].SweetnesId && wine.SourceId == wvm.ListUserSourceTaste[2].SourceId)
                {
                    listOne.Add(wine);
                }
                else if (wine.ColourId == wvm.ListUserColourTaste[0].ColourId && wine.SweetnesId == wvm.ListUserSweetnessTaste[0].SweetnesId && wine.SourceId == wvm.ListUserSourceTaste[0].SourceId)
                {
                    listTwo.Add(wine);
                }
                else if (wine.ColourId == wvm.ListUserColourTaste[1].ColourId && wine.SweetnesId == wvm.ListUserSweetnessTaste[1].SweetnesId && wine.SourceId == wvm.ListUserSourceTaste[1].SourceId)
                {
                    listTwo.Add(wine);
                }
                else if (wine.ColourId == wvm.ListUserColourTaste[2].ColourId && wine.SweetnesId == wvm.ListUserSweetnessTaste[2].SweetnesId && wine.SourceId == wvm.ListUserSourceTaste[2].SourceId)
                {
                    listTwo.Add(wine);
                }
                else if (wine.SourceId == wvm.Wine.SourceId && wine.SweetnesId == wvm.ListUserSweetnessTaste[0].SweetnesId)
                {
                    listThree.Add(wine);
                }
                else if (wine.SourceId == wvm.Wine.SourceId && wine.SweetnesId == wvm.ListUserSweetnessTaste[1].SweetnesId)
                {
                    listThree.Add(wine);
                }
                else if (wine.SourceId == wvm.Wine.SourceId && wine.SweetnesId == wvm.ListUserSweetnessTaste[2].SweetnesId)
                {
                    listThree.Add(wine);
                }
                else if (wine.SweetnesId == wvm.ListUserSweetnessTaste[0].SweetnesId)
                {
                    listTwo.Add(wine);
                }
                else if (wine.SourceId == wvm.ListUserSourceTaste[0].SourceId)
                {
                    listThree.Add(wine);
                }
            }

            Random rnd = new Random();
            wvm.ListWinesTastesDetails = new List<Wine>();
            for (int i = 0; i < 5; i++)
            {
                if (listOne.Count != 0 && listOne.Count > i)
                {
                    bool salir = false;
                    do
                    {
                        int numberRandom = rnd.Next(0, listOne.Count - 1);
                        if (!wvm.ListWinesTastesDetails.Contains(listOne[numberRandom]))
                        {
                            wvm.ListWinesTastesDetails.Add(listOne[numberRandom]);
                            salir = true;
                        }
                    } while (!salir);
                }
                else
                {
                    break;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (listTwo.Count != 0 && listTwo.Count>i)
                {
                    bool salir = false;
                    do
                    {
                        int numberRandom = rnd.Next(0, listTwo.Count - 1);
                        if (!wvm.ListWinesTastesDetails.Contains(listTwo[numberRandom]))
                        {
                            wvm.ListWinesTastesDetails.Add(listTwo[numberRandom]);
                            salir = true;
                        }
                    } while (!salir);
                }
                else
                {
                    break;
                }
            }
            for (int i = 0; i < 2; i++)
            {
                if (listThree.Count != 0 && listThree.Count > i)
                {
                    bool salir = false;
                    do
                    {
                        int numberRandom = rnd.Next(0, listThree.Count - 1);
                        if (!wvm.ListWinesTastesDetails.Contains(listThree[numberRandom]))
                        {
                            wvm.ListWinesTastesDetails.Add(listThree[numberRandom]);
                            salir = true;
                        }
                    } while (!salir);
                }
                else
                {
                    break;
                }
            }

            if (wvm.ListWinesTastesDetails.Count == 0)
            {
                return NotFound();
            }

            //lista de los vinos mejor valorados de orde de mayor a menor
            wvm.ListWineUserScore = await _winesServices.GetWinesAsync();
            wvm.ListWineUserScore = wvm.ListWineUserScore.OrderByDescending(x => x.Score).ToList().GetRange(0,10);

            return View(wvm);
        }

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
