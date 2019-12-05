using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        public TastesController(ITastes tastesServices, IFilters filtersServices, UserManager<AppUser> userManager)
        {
            _tastesServices = tastesServices;
            _filtersServices = filtersServices;
            _userManager = userManager;
        }

        // GET: Tastes
        [Authorize]
        public async Task<ActionResult> Survey()
        {
            List<SourceTaste> sourcetaste = await _tastesServices.GetSourceTastesAsync();
            List<ColourTaste> colourtaste = await _tastesServices.GetColourTastesAsync();
            List<SweetnessTaste> sweetnesstaste = await _tastesServices.GetSweetnessTastesAsync();
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            TastesVM tvm = new TastesVM
            {
                ListColours = await _filtersServices.GetColourAsync(),
                ListSources = await _filtersServices.GetSourceAsync(),
                ListSweetness = await _filtersServices.GetSweetnesAsync(),

                SourceTastes = sourcetaste.Where(x => x.AppUser.Id == user.Id).ToList(),
                ColourTastes = colourtaste.Where(x => x.AppUser.Id == user.Id).ToList(),
                SweetnesTastes = sweetnesstaste.Where(x => x.AppUser.Id == user.Id).ToList(),

            };

            return View(tvm);
        }

        //Inserta los valores en las tablas Tastes cuando el usuario rellena la encuesta, 
        //en este caso el valor siempre es 5,
        //independientemente de su valoración total.
        public async Task<ActionResult> InsertSurveyValues(int[] colour, int[] source, int[] sweet)
        {
            if (colour.Length > 0)
            {
                foreach (int col in colour)
                {
                    ColourTaste colourTaste = await _tastesServices.GetColourTasteByIdAsync(col);
                    colourTaste.Score += 5;
                    await _tastesServices.UpdateColourTasteAsync(colourTaste);
                }
            }
            if (source.Length > 0)
            {
                foreach (int sor in source)
                {
                    SourceTaste sourceTaste = await _tastesServices.GetSourceTasteByIdAsync(sor);
                    sourceTaste.Score += 5;
                    await _tastesServices.UpdateSourceTasteAsync(sourceTaste);
                }

            }
            if (sweet.Length > 0)
            {
                foreach (int swe in sweet)
                {
                    SweetnessTaste sweetnessTaste = await _tastesServices.GetSweetnessTasteByIdAsync(swe);
                    sweetnessTaste.Score += 5;
                    await _tastesServices.UpdateSweetnessTasteAsync(sweetnessTaste);
                }

            }
            AppUser user = await _userManager.GetUserAsync(User);
            user.Survey = true;
            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(HomeController));
        }


        //Inserta los valores en las tablas Tastes cuando el usuario hace click, 
        //el valor base es +1
        //el valor que se reduce de manera exponencial cuando la puntuación total supera los 10 puntos,
        public async Task InsertClickValues(int colourId, int sourceId, int sweetId)
        {
            AppUser user = await _userManager.FindByEmailAsync(User.Identity.Name);
            List<ColourTaste> colourTastes = await _tastesServices.GetColourTasteesByUserIdAsync(user.Id);
            List<SourceTaste> sourceTastes = await _tastesServices.GetSourceTasteesByUserIdAsync(user.Id);
            List<SweetnessTaste> sweetnessTastes = await _tastesServices.GetSweetnessTasteesByUserIdAsync(user.Id);

            if (colourId != 0)
            {
                ColourTaste colourTaste = colourTastes.FirstOrDefault(x => x.ColourId == colourId);
                if (colourTaste.Score < 10)
                {
                    colourTaste.Score += 1;
                    await _tastesServices.UpdateColourTasteAsync(colourTaste);
                }
                else if (colourTaste.Score >= 10)
                {
                    double root = Convert.ToDouble(colourTaste.Score.ToString().Substring(0, 1)) + 1;
                    double addScore = Math.Sqrt(1/(Math.Pow(root,root)));
                    colourTaste.Score += addScore;
                }
            }

            if (sourceId != 0)
            {
                SourceTaste sourceTaste = sourceTastes.FirstOrDefault(x => x.SourceId == sourceId);

                if (sourceTaste.Score < 10)
                {
                    sourceTaste.Score += 0.99;
                    await _tastesServices.UpdateSourceTasteAsync(sourceTaste);
                }
                else if (sourceTaste.Score >= 10)
                {
                    double root = Convert.ToDouble(sourceTaste.Score.ToString().Substring(0, 1)) + 1;
                    double addScore = Math.Sqrt(1 / (Math.Pow(root, root)));
                    sourceTaste.Score += addScore;
                    await _tastesServices.UpdateSourceTasteAsync(sourceTaste);
                }
            }

            if (sweetId != 0)
            {
                SweetnessTaste sweetnessTaste = sweetnessTastes.FirstOrDefault(x => x.SweetnesId == sweetId);

                if (sweetnessTaste.Score < 10)
                {
                    sweetnessTaste.Score += 1;
                    await _tastesServices.UpdateSweetnessTasteAsync(sweetnessTaste);
                }
                else if (sweetnessTaste.Score >= 10)
                {
                    double root = Convert.ToDouble(sweetnessTaste.Score.ToString().Substring(0, 1)) + 1;
                    double addScore = Math.Sqrt(1 / (Math.Pow(root, root)));
                    sweetnessTaste.Score += addScore;
                    await _tastesServices.UpdateSweetnessTasteAsync(sweetnessTaste);
                }
            }

        }



        //Inserta los valores en las tablas Tastes cuando el usuario añade un vino a favoritos, 
        //el valor base es +4
        //el valor que se reduce de manera exponencial cuando la puntuación total supera los 10 puntos,
        public async Task InsertFavouriteValues(int colourId, int sourceId, int sweetId)
        {
            AppUser user = await _userManager.FindByEmailAsync(User.Identity.Name);
            List<ColourTaste> colourTastes = await _tastesServices.GetColourTasteesByUserIdAsync(user.Id);
            List<SourceTaste> sourceTastes = await _tastesServices.GetSourceTasteesByUserIdAsync(user.Id);
            List<SweetnessTaste> sweetnessTastes = await _tastesServices.GetSweetnessTasteesByUserIdAsync(user.Id);

            if (colourId != 0)
            {
                ColourTaste colourTaste = colourTastes.FirstOrDefault(x => x.ColourId == colourId);
                if (colourTaste.Score < 10)
                {
                    colourTaste.Score += 4;
                    await _tastesServices.UpdateColourTasteAsync(colourTaste);
                }
                else if (colourTaste.Score >= 10)
                {
                    double root = Convert.ToDouble(colourTaste.Score.ToString().Substring(0, 1)) + 1;
                    double addScore = Math.Sqrt(4 / (Math.Pow(root, root)));
                    colourTaste.Score += addScore;
                }
            }

            if (sourceId != 0)
            {
                SourceTaste sourceTaste = sourceTastes.FirstOrDefault(x => x.SourceId == sourceId);

                if (sourceTaste.Score < 10)
                {
                    sourceTaste.Score += 4;
                    await _tastesServices.UpdateSourceTasteAsync(sourceTaste);
                }
                else if (sourceTaste.Score >= 10)
                {
                    double root = Convert.ToDouble(sourceTaste.Score.ToString().Substring(0, 1)) + 1;
                    double addScore = Math.Sqrt(4 / (Math.Pow(root, root)));
                    sourceTaste.Score += addScore;
                    await _tastesServices.UpdateSourceTasteAsync(sourceTaste);
                }
            }

            if (sweetId != 0)
            {
                SweetnessTaste sweetnessTaste = sweetnessTastes.FirstOrDefault(x => x.SweetnesId == sweetId);

                if (sweetnessTaste.Score < 10)
                {
                    sweetnessTaste.Score += 4;
                    await _tastesServices.UpdateSweetnessTasteAsync(sweetnessTaste);
                }
                else if (sweetnessTaste.Score >= 10)
                {
                    double root = Convert.ToDouble(sweetnessTaste.Score.ToString().Substring(0, 1)) + 1;
                    double addScore = Math.Sqrt(4 / (Math.Pow(root, root)));
                    sweetnessTaste.Score += addScore;
                    await _tastesServices.UpdateSweetnessTasteAsync(sweetnessTaste);
                }
            }

        }



        //Inserta los valores en las tablas Tastes cuando el usuario hace click, 
        //el valor base es +3
        //el valor que se reduce de manera exponencial cuando la puntuación total supera los 10 puntos,
        public async Task InsertLikeValues(int colourId, int sourceId, int sweetId)
        {
            AppUser user = await _userManager.FindByEmailAsync(User.Identity.Name);
            List<ColourTaste> colourTastes = await _tastesServices.GetColourTasteesByUserIdAsync(user.Id);
            List<SourceTaste> sourceTastes = await _tastesServices.GetSourceTasteesByUserIdAsync(user.Id);
            List<SweetnessTaste> sweetnessTastes = await _tastesServices.GetSweetnessTasteesByUserIdAsync(user.Id);

            if (colourId != 0)
            {
                ColourTaste colourTaste = colourTastes.FirstOrDefault(x => x.ColourId == colourId);
                if (colourTaste.Score < 10)
                {
                    colourTaste.Score += 3;
                    await _tastesServices.UpdateColourTasteAsync(colourTaste);
                }
                else if (colourTaste.Score >= 10)
                {
                    double root = Convert.ToDouble(colourTaste.Score.ToString().Substring(0, 1)) + 1;
                    double addScore = Math.Sqrt(3 / (Math.Pow(root, root)));
                    colourTaste.Score += addScore;
                }
            }

            if (sourceId != 0)
            {
                SourceTaste sourceTaste = sourceTastes.FirstOrDefault(x => x.SourceId == sourceId);

                if (sourceTaste.Score < 10)
                {
                    sourceTaste.Score += 3;
                    await _tastesServices.UpdateSourceTasteAsync(sourceTaste);
                }
                else if (sourceTaste.Score >= 10)
                {
                    double root = Convert.ToDouble(sourceTaste.Score.ToString().Substring(0, 1)) + 1;
                    double addScore = Math.Sqrt(3 / (Math.Pow(root, root)));
                    sourceTaste.Score += addScore;
                    await _tastesServices.UpdateSourceTasteAsync(sourceTaste);
                }
            }

            if (sweetId != 0)
            {
                SweetnessTaste sweetnessTaste = sweetnessTastes.FirstOrDefault(x => x.SweetnesId == sweetId);

                if (sweetnessTaste.Score < 10)
                {
                    sweetnessTaste.Score += 3;
                    await _tastesServices.UpdateSweetnessTasteAsync(sweetnessTaste);
                }
                else if (sweetnessTaste.Score >= 10)
                {
                    double root = Convert.ToDouble(sweetnessTaste.Score.ToString().Substring(0, 1)) + 1;
                    double addScore = Math.Sqrt(3 / (Math.Pow(root, root)));
                    sweetnessTaste.Score += addScore;
                    await _tastesServices.UpdateSweetnessTasteAsync(sweetnessTaste);
                }
            }

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