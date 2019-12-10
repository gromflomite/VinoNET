using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Wineapp.Models;
using Wineapp.Models.ViewModels;
using Wineapp.Services;

namespace Wineapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITastes _tastesServices;
        private readonly IWines _winesServices;
        private readonly IFilters _filtersServices;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, ITastes tastesServices, IWines winesServices, IFilters filtersServices, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _tastesServices = tastesServices;
            _winesServices = winesServices;
            _filtersServices = filtersServices;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            WinesVM wvm = await WineSourceIndexList();
            return View(wvm);
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
        public async Task<WinesVM> WineIndexList()
        {
            if (_signInManager.IsSignedIn(User))
            {
                WinesVM wvm = await GetUserPreferences();
                List<Wine> wineTasteList = new List<Wine>();

                foreach (Wine wine in await _winesServices.GetWinesAsync())
                {
                    if (wine.ColourId == wvm.ListUserColourTaste[0].ColourId && wine.SweetnesId == wvm.ListUserSweetnessTaste[0].SweetnesId && wine.SourceId == wvm.ListUserSourceTaste[0].SourceId)
                    {
                        wineTasteList.Add(wine);
                    }
                    else if (wine.ColourId == wvm.ListUserColourTaste[0].ColourId && wine.SweetnesId == wvm.ListUserSweetnessTaste[0].SweetnesId && wine.SourceId == wvm.ListUserSourceTaste[1].SourceId)
                    {
                        wineTasteList.Add(wine);
                    }
                    else if (wine.ColourId == wvm.ListUserColourTaste[1].ColourId && wine.SweetnesId == wvm.ListUserSweetnessTaste[1].SweetnesId && wine.SourceId == wvm.ListUserSourceTaste[2].SourceId)
                    {
                        wineTasteList.Add(wine);
                    }
                }

                Random rnd = new Random();
                wvm.ListWineTaste = new List<Wine>();
                for (int i = 0; i < 3; i++)
                {
                    if (wineTasteList.Count != 0 && wineTasteList.Count > i)
                    {
                        bool salir = false;
                        do
                        {
                            int numberRandom = rnd.Next(0, wineTasteList.Count - 1);
                            if (!wvm.ListWineTaste.Contains(wineTasteList[numberRandom]))
                            {
                                wvm.ListWineTaste.Add(wineTasteList[numberRandom]);
                                salir = true;
                            }
                        } while (!salir);
                    }
                    else
                    {
                        break;
                    }
                }
                return wvm;

            }
            else
            {
                WinesVM wvm = new WinesVM();
                wvm.ListWineTaste = await _winesServices.GetWinesAsync();
                wvm.ListWineTaste = wvm.ListWineTaste.OrderByDescending(x => x.Score).ToList().GetRange(0,3);
                return wvm;
            }

        }

        public async Task<WinesVM> WineSourceIndexList()
        {
            WinesVM wvm = await WineIndexList();
            if (_signInManager.IsSignedIn(User))
            {

                List<Source> getSources = await _filtersServices.GetSourceAsync();
                wvm.ListSources = new List<Source>();

                if (_signInManager.IsSignedIn(User))
                {
                    foreach (Source source in getSources)
                    {
                        if (wvm.ListUserSourceTaste[0].SourceId == source.Id)
                        {
                            wvm.ListSources.Add(source);
                        }
                        else if (wvm.ListUserSourceTaste[1].SourceId == source.Id)
                        {
                            wvm.ListSources.Add(source);
                        }
                    }
                }
                Random rnd = new Random();

                int numberRandom = rnd.Next(0, getSources.Count - 1);
                bool salir = false;
                do
                {
                    if (!wvm.ListSources.Contains(getSources[numberRandom]))
                    {
                        wvm.ListSources.Add(getSources[numberRandom]);
                        salir = true;
                    }

                } while (!salir);

                return wvm;
            }
            else
            {
                List<Source> sources = await _filtersServices.GetSourceAsync();
                wvm.ListSources = new List<Source>();
                Random rnd = new Random();

                int cont = 0;
                do
                {
                    int numberRandom = rnd.Next(0, sources.Count - 1);
                    if (!wvm.ListSources.Contains(sources[numberRandom]))
                    {
                        wvm.ListSources.Add(sources[numberRandom]);
                        cont++;
                    }

                } while (cont < 4);

                return wvm;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Hello()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
