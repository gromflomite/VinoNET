using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Wineapp.Models;
using Wineapp.Models.ViewModels;
using Wineapp.Services;

namespace Wineapp.Controllers
{
    public class TastesController : Controller
    {
        private readonly ITastes _tastesServices;
        private readonly IFilters _filtersServices;
        private readonly UserManager<AppUser> _userManager;

        public TastesController(ITastes tastesServices, IFilters filtersServices)
        {
            _tastesServices = tastesServices;
            _filtersServices = filtersServices;
        }

        // GET: Tastes
        public async Task<ActionResult> Survey(string userId)
        {

            List<SourceTaste> sourcetaste = await _tastesServices.GetSourceTastesAsync();
            List<ColourTaste> colourtaste = await _tastesServices.GetColourTastesAsync();
            List<SweetnessTaste> sweetnesstaste = await _tastesServices.GetSweetnessTastesAsync();

            TastesVM tvm = new TastesVM
            {
                ListColours = await _filtersServices.GetColourAsync(),
                ListSources = await _filtersServices.GetSourceAsync(),
                ListSweetness = await _filtersServices.GetSweetnesAsync(),

                SourceTastes = sourcetaste.Where(x => x.AppUser.Id == _userManager.GetUserId(User)).ToList(),
                ColourTastes = colourtaste.Where(x => x.AppUser.Id == _userManager.GetUserId(User)).ToList(),
                SweetnesTastes = sweetnesstaste.Where(x => x.AppUser.Id == _userManager.GetUserId(User)).ToList(),

            };

            return View(tvm);
        }

        public async Task<ActionResult> InsertSurveyValues(int[] colour, int[] source, int[] sweet)
        {
            foreach (int col in colour)
            {
                ColourTaste colourTaste = await _tastesServices.GetColourTasteByIdAsync(col);
                colourTaste.Score = +5;
                await _tastesServices.UpdateColourTasteAsync(colourTaste);
            }
            foreach (int sor in source)
            {
                SourceTaste sourceTaste = await _tastesServices.GetSourceTasteByIdAsync(sor);
                sourceTaste.Score = +5;
                await _tastesServices.UpdateSourceTasteAsync(sourceTaste);
            }
            foreach (int swe in sweet)
            {
                SweetnessTaste sweetnessTaste = await _tastesServices.GetSweetnessTasteByIdAsync(swe);
                sweetnessTaste.Score = +5;
                await _tastesServices.UpdateSweetnessTasteAsync(sweetnessTaste);
            }
            AppUser user = await _userManager.GetUserAsync(User);
            user.Survey = true;
            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index));
        }


        // GET: Tastes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tastes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tastes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Tastes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tastes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Tastes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tastes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}