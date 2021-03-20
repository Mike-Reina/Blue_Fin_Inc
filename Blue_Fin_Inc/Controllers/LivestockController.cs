using Blue_Fin_Inc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blue_Fin_Inc.Controllers
{
    public class LivestockController : Controller
    {
        // GET: LivestockController
        public ActionResult Index()
        {
            return View();
        }

        // GET: LivestockController/Details/5
        public ActionResult Details()
        {
            Livestock livestock1 = new Livestock(CareLevel.Easy, Temperment.Peaceful, WaterType.Fresh, "Black, Silver, Red", "PH:6.0-6.5, KH 0-10, 22°C-26°C", "5cm", 2001, "Harlequin Rasbora", "The Harlequin Rasbora is easily identified by its characteristic black pork chop shaped patch and beautifully lustrous copper/orange body", 2.99);
            livestock1.AddStock(10);
            return View(livestock1);
        }

        // GET: LivestockController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LivestockController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LivestockController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LivestockController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LivestockController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LivestockController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
