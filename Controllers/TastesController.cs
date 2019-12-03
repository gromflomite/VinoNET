using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wineapp.Models.ViewModels;
using Wineapp.Services;

namespace Wineapp.Controllers
{
    public class TastesController : Controller
    {
        private readonly ITastes _tastesServices;

        public TastesController(ITastes tastesServices)
        {
            _tastesServices = tastesServices;
        }

        // GET: Tastes
        public ActionResult Survey(string userId)
        {
            TastesVM tvm = new TastesVM
            {
             

            };

            return View(tvm);
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