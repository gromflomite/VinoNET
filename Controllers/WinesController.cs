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
    public class WinesController : Controller
    {
        private readonly ITastes _tastesServices;
        private readonly IWines _winesServices;
        private readonly IFilters _filtersServices;
        private readonly ILike _likeServices;
        private readonly IWineLists _wineListsServices;

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;


        public WinesController(ILike likeServices, ITastes tastesServices, IWines winesServices, IFilters filtersServices, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _tastesServices = tastesServices;
            _winesServices = winesServices;
            _filtersServices = filtersServices;
            _likeServices = likeServices;
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
            if (_signInManager.IsSignedIn(User))
            {

                WinesVM wvm = await GetUserPreferences();
                wvm.Source = await _filtersServices.GetSourceByIdAsync(sourceId);
                wvm.ListSources = await _filtersServices.GetSourceAsync();

                List<Wine> wines = await _winesServices.GetWinesAsync();
                wines = wines.Where(x => x.Source.Id == sourceId).ToList();
                AppUser user = await _userManager.FindByEmailAsync(User.Identity.Name);
                await _tastesServices.InsertClickValues(0, wvm.Source.Id, 0, 1, user.Id);

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

                wvm.ListWinesTastesSources = new List<Wine>();

                Random numberRandom = new Random();
                if (firstPreference.Count > 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (firstPreference.Count != 0 && firstPreference.Count > i)
                        {
                            bool salir = false;
                            do
                            {
                                int num = numberRandom.Next(0, firstPreference.Count - 1);
                                if (!wvm.ListWinesTastesSources.Contains(firstPreference[num]))
                                {
                                    wvm.ListWinesTastesSources.Add(firstPreference[num]);
                                    salir = true;
                                }
                            } while (!salir);
                        }
                        else
                        {
                            break;
                        }
                    }

                }
                if (secondPreference.Count > 0)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        if (firstPreference.Count != 0 && firstPreference.Count > i)
                        {
                            bool salir = false;
                            do
                            {
                                int num = numberRandom.Next(0, secondPreference.Count - 1);
                                if (!wvm.ListWinesTastesSources.Contains(secondPreference[num]))
                                {
                                    wvm.ListWinesTastesSources.Add(secondPreference[num]);
                                    salir = true;
                                }
                            } while (!salir);
                        }
                        else
                        {
                            break;
                        }

                    }

                }

                if (wvm.ListWinesTastesSources == null)
                {
                    wvm.ListWinesTastesSources = wines;
                }

                return View(wvm);

            }
            else
            {
                WinesVM wvm = new WinesVM();
                wvm.Source = await _filtersServices.GetSourceByIdAsync(sourceId);
                wvm.ListWinesTastesSources = await _winesServices.GetWinesAsync();
                wvm.ListWinesTastesSources = wvm.ListWinesTastesSources.Where(x => x.SourceId == sourceId).ToList();
                wvm.ListWinesTastesSources = wvm.ListWinesTastesSources.OrderByDescending(x => x.Score).ToList().GetRange(0, 5);
                wvm.ListSources = await _filtersServices.GetSourceAsync();
                wvm.ListSources = wvm.ListSources.GetRange(0, 6);

                return View(wvm);
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (_signInManager.IsSignedIn(User))
            {
                WinesVM wvm = await GetUserPreferences();
                wvm.Wine = await _winesServices.GetWineByIdAsync(id);
                AppUser user = await _userManager.FindByEmailAsync(User.Identity.Name);
                await _tastesServices.InsertClickValues(wvm.Wine.ColourId, wvm.Wine.SourceId, wvm.Wine.SweetnesId, 1, user.Id);


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
                    if (listTwo.Count != 0 && listTwo.Count > i)
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
                wvm.ListWineUserScore = wvm.ListWineUserScore.OrderByDescending(x => x.Score).ToList().GetRange(0, 10);

                return View(wvm);

            }
            else
            {
                WinesVM wvm = new WinesVM();
                wvm.Wine = await _winesServices.GetWineByIdAsync(id);
                wvm.ListWineUserScore = await _winesServices.GetWinesAsync();
                wvm.ListWineUserScore = wvm.ListWineUserScore.OrderByDescending(x => x.Score).ToList().GetRange(0, 10);
                return View(wvm);
            }

        }

        [Authorize]
        public async Task<IActionResult> ViewsFavorite(int? id)
        {
            AppUser user = await _userManager.FindByEmailAsync(User.Identity.Name);

            bool exitWineListsWine = _wineListsServices.Exit(id, user.Id);

            Wine wine = await _winesServices.GetWineByIdAsync(id);
            WinesVM wvm = new WinesVM
            {
                ListWinesLists = await _wineListsServices.GetWineListsByUserIdAsync(user.Id),
                Wine = wine,
                WinelistsWineExit = exitWineListsWine
            };
            if (exitWineListsWine == true)
            {
                wvm.WineListWine = await _wineListsServices.IfExitWineListWine(id, user.Id);
            }
            return View(wvm);
        }
        [Authorize]
        public async Task InsertLikeValues(int colourId, int sourceId, int sweetId, string url, int idWine)
        {
            AppUser user = await _userManager.FindByEmailAsync(User.Identity.Name);
            await _tastesServices.InsertClickValues(colourId, sourceId, sweetId, 3, user.Id);

            UserScore userScore = new UserScore();
            userScore.VoteValue = 0;
            userScore.VoteDate = DateTime.Now;
            userScore.AppUserId = user.Id;
            userScore.WineId = idWine;

            await _likeServices.Create(userScore);

            Wine wine = await _winesServices.GetWineByIdAsync(idWine);
            wine.Score++;
            await _winesServices.UpdateWineAsync(wine);

            Response.Redirect(url);
        }
        [Authorize]
        public async Task DelateLikeValues(int colourId, int sourceId, int sweetId, string url, int idWine)
        {
            AppUser user = await _userManager.FindByEmailAsync(User.Identity.Name);
            await _tastesServices.DelateLikeValues(colourId, sourceId, sweetId, user.Id);
            await _likeServices.Delete(idWine, user.Id);

            Wine wine = await _winesServices.GetWineByIdAsync(idWine);
            wine.Score--;
            await _winesServices.UpdateWineAsync(wine);

            Response.Redirect(url);
        }

        public async Task AddWineInLists(int listaId, string url, int idWine)
        {
            Wine wine = await _winesServices.GetWineByIdAsync(idWine);
            AppUser user = await _userManager.FindByEmailAsync(User.Identity.Name);
            await _tastesServices.InsertClickValues(wine.ColourId, wine.SourceId, wine.SweetnesId, 4, user.Id);
            WineListWine wineListWine = new WineListWine
            {
                WineId = idWine,
                WineListId = listaId
            };
            await _wineListsServices.AddWineInWineListAsync(wineListWine);
            Response.Redirect(url);
        }
        [HttpPost]
        public async Task DelateWineListWineValues(int wineListWineId, string url, int idWine)
        {
            Wine wine = await _winesServices.GetWineByIdAsync(idWine);
            AppUser user = await _userManager.FindByEmailAsync(User.Identity.Name);
            await _tastesServices.DelateWineListWineValues(wine.ColourId, wine.SourceId, wine.SweetnesId, user.Id);


            await _wineListsServices.DeleteWineListWineAsync(await _wineListsServices.GetWineListWineByIdAsync(wineListWineId));

            Response.Redirect(url);
        }


        //GET: Wines
        public async Task<IActionResult> Index()
        {
            List<Wine> listWines = await _winesServices.GetWinesAsync();
            return View(listWines);
        }

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
