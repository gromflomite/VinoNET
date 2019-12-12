﻿using System;
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
    public class AdminInfoController : Controller
    {

        private readonly ITastes _tastesServices;
        private readonly IWines _winesServices;
        private readonly IFilters _filtersServices;
        private readonly ILike _likeServices;
        private readonly UserManager<AppUser> _userManager;

        public AdminInfoController(ITastes tastesServices, IWines winesServices, IFilters filtersServices, ILike likeServices, UserManager<AppUser> userManager)
        {
            _tastesServices = tastesServices;
            _winesServices = winesServices;
            _filtersServices = filtersServices;
            _likeServices = likeServices;
            _userManager = userManager;
        }

        // Genera los datos para mostrar en la vista
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            AdminVM avm = await MostVisitedWines();
            
            return View(avm);
        }
        public async Task<AdminVM> MostVisitedWines()
        {
            AdminVM avm = new AdminVM();
            List<ColourTaste> colourTasteList = await _tastesServices.GetColourTastesAsync();

            avm.colourNames = colourTasteList.Select(x => x.Colour.ColourType).Distinct().ToArray();
            avm.colourScores = new double[avm.colourNames.Length];

            foreach (ColourTaste colourTaste in colourTasteList)
            {
                for (int i = 1; i <= avm.colourNames.Length; i++)
                {
                    if (colourTaste.ColourId == i)
                    {
                        avm.colourScores[i-1] += colourTaste.Score;
                    }
                }
            }

            return await MostVisitedSources(avm);
        }
        public async Task<AdminVM> MostVisitedSources(AdminVM avm)
        {           
            List<SourceTaste> sourceTasteList = await _tastesServices.GetSourceTastesAsync();

            avm.sourceNames = sourceTasteList.Select(x => x.Source.SourceType).Distinct().ToArray();
            avm.sourceScores = new double[avm.sourceNames.Length];

            foreach (SourceTaste sourceTaste in sourceTasteList)
            {
                for (int i = 1; i <= avm.colourNames.Length; i++)
                {
                    if (sourceTaste.SourceId == i)
                    {
                        avm.sourceScores[i-1] += sourceTaste.Score;
                    }
                }

            }
            return await TopWines(avm);
        }

        public async Task<AdminVM> TopWines(AdminVM avm)
        {           

            avm.topWines = await _winesServices.GetWinesAsync();
            avm.topWines = avm.topWines.OrderByDescending(x => x.Score).ToList().GetRange(0, 10);

            return await TopFiveWinesWeek(avm);

        }
        public async Task<AdminVM> TopFiveWinesWeek(AdminVM avm)
        {           
            List<UserScore> userScores = await _likeServices.GetUserScoreAsync();
            userScores = userScores.Where(x => x.VoteDate < DateTime.Now.AddDays(-7)).ToList();

            //Lista de tipo Wine
            avm.winesName = userScores.Select(x => x.Wine.Name).Distinct().ToList();
            //Array doble de int
            //avm.winesScore = new int[avm.winesName.Count, avm.winesName.Count];
            avm.winesScore= new Dictionary<int, string>();

            foreach (UserScore userScore in userScores)
            {
                for (int i = 0; i < avm.winesName.Count; i++)
                {
                    int score = userScores.Where(x => x.Wine.Name == avm.winesName[i]).Count();
                    string wine = avm.winesName[i];
                    avm.winesScore.Add(score, wine);
                }
            }
            avm.winesScore = avm.winesScore.OrderByDescending(x => x.Key).ToDictionary(x=>x.Key,y=>y.Value);
            avm.winesScore.Skip(0).Take(5).ToDictionary(x => x.Key, y => y.Value);
            return avm;
        }

    }
}