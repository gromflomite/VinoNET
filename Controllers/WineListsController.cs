using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    public class WineListsController : Controller
    {
        private readonly ITastes _tastesServices;
        private readonly IWines _winesServices;
        private readonly IFilters _filtersServices;
        private readonly ILike _likeServices;
        private readonly IWineLists _wineListsServices;

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;


        public WineListsController(IWineLists wineListsServices, ILike likeServices, ITastes tastesServices, IWines winesServices, IFilters filtersServices, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _wineListsServices = wineListsServices;
            _tastesServices = tastesServices;
            _winesServices = winesServices;
            _filtersServices = filtersServices;
            _likeServices = likeServices;
            _userManager = userManager;
            _signInManager = signInManager;

        }

        // GET: WineLists
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);

            List<WineList> listWineList = await _wineListsServices.GetWineListsByUserIdAsync(user.Id);           
            WinesVM wvm = new WinesVM
            {
                ListWinesLists = listWineList,
                AppUser = user
            };
            return View(wvm);
        }
        
        public async Task<IActionResult> DeleteWineList(int winwListId)
        {
            WineList wineList = await _wineListsServices.GetWineListByIdAsync(winwListId);
            await _wineListsServices.DeleteWineListAsync(wineList);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DelateWineListWineValues(int wineListWineId, int idWine)
        {
            Wine wine = await _winesServices.GetWineByIdAsync(idWine);
            AppUser user = await _userManager.FindByEmailAsync(User.Identity.Name);
            await _tastesServices.DelateWineListWineValues(wine.ColourId, wine.SourceId, wine.SweetnesId, user.Id);     
            await _wineListsServices.DeleteWineListWineAsync(await _wineListsServices.GetWineListWineByIdAsync(wineListWineId));

            return RedirectToAction("Index");
        }


        // GET: WineLists/Details/5
        [Authorize]        
        public async Task<IActionResult> Details(string nameList)
        {
            AppUser user = await _userManager.FindByEmailAsync(User.Identity.Name);
            List<WineList> wineLists = await _wineListsServices.GetWineListsByUserIdAsync(user.Id);
            WineList wineList = await _wineListsServices.GetWineListByNameListAsync(nameList);

            if (wineList.Id == 0)
            {
                return NotFound();
            }

            WinesVM winesVMx = new WinesVM
            {
                WineList = wineList,
                AppUser = user,
                ListWinesListWines = await _wineListsServices.GetWineListsWinesByWineLisIdAsync(wineList.Id)            
            };
            
            if (winesVMx == null)
            {
                return NotFound();
            }

            return View(winesVMx);
        }

        // GET: WineLists/Create
        public IActionResult Create()
        {            
            return View(new WineList());
        }

        // POST: WineLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ListName,Description")] WineList wineList)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            wineList.AppUserId = user.Id;
            wineList.ListDate = DateTime.Now;
            if (ModelState.IsValid)
            {
               await _wineListsServices.CreateWineListAsync(wineList);                
                return RedirectToAction(nameof(Index));
            }

            return View(wineList);
        }
        [Authorize]
        public async Task MoveWine(string url, int listaId, int wineListWineId)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);

            WineListWine wineListWine = await _wineListsServices.GetWineListWineByIdAsync(wineListWineId);

            wineListWine.WineListId = listaId;
            if (ModelState.IsValid)
            {
                await _wineListsServices.MoveWine(wineListWine);
                Response.Redirect(url);
            }

            Response.Redirect(url);
        }

        //    // GET: WineLists/Edit/5
        //    public async Task<IActionResult> Edit(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var wineList = await _context.WineLists.FindAsync(id);
        //        if (wineList == null)
        //        {
        //            return NotFound();
        //        }
        //        ViewData["AppUserId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", wineList.AppUserId);
        //        return View(wineList);
        //    }

        //    // POST: WineLists/Edit/5
        //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Edit(int id, [Bind("Id,ListName,Description,ListDate,AppUserId")] WineList wineList)
        //    {
        //        if (id != wineList.Id)
        //        {
        //            return NotFound();
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                _context.Update(wineList);
        //                await _context.SaveChangesAsync();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!WineListExists(wineList.Id))
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }
        //            return RedirectToAction(nameof(Index));
        //        }
        //        ViewData["AppUserId"] = new SelectList(_context.Set<AppUser>(), "Id", "Id", wineList.AppUserId);
        //        return View(wineList);
        //    }

        //    // GET: WineLists/Delete/5
        //    public async Task<IActionResult> Delete(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var wineList = await _context.WineLists
        //            .Include(w => w.AppUser)
        //            .FirstOrDefaultAsync(m => m.Id == id);
        //        if (wineList == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(wineList);
        //    }

        //    // POST: WineLists/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> DeleteConfirmed(int id)
        //    {
        //        var wineList = await _context.WineLists.FindAsync(id);
        //        _context.WineLists.Remove(wineList);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    private bool WineListExists(int id)
        //    {
        //        return _context.WineLists.Any(e => e.Id == id);
        //    }
    }
}
